package org.onepf.opfpush.unity.utils;

import android.content.Context;
import android.support.annotation.NonNull;
import com.unity3d.player.UnityPlayer;
import org.onepf.opfutils.OPFLog;
import org.onepf.opfutils.OPFPreferences;

import java.util.Comparator;
import java.util.HashSet;
import java.util.Set;
import java.util.TreeSet;

import static java.util.Locale.US;

/**
 * @author Roman Savin
 * @since 20.07.2015
 */
public final class MessagesDeliveryController {

    private static final String OPF_UNITY_POSTFIX = "opfpush-unity";
    private static final String EVENT_RECEIVER = "OPFPush";
    private static final String UNDELIVERED_MESSAGES_KEYS = "UNDELIVERED_MESSAGES_KEYS";
    private static final String UNDELIVERED_MESSAGE_CALLBACK_POSTFIX = "-callback";
    private static final String UNDELIVERED_MESSAGE_BODY_POSTFIX = "-body";

    private MessagesDeliveryController() {
        throw new UnsupportedOperationException();
    }

    public static void sendUnityMessage(@NonNull final Context context,
                                        @NonNull final String callbackMethod,
                                        @NonNull final String message) {
        try {
            UnityPlayer.UnitySendMessage(EVENT_RECEIVER, callbackMethod, message);
        } catch (UnsatisfiedLinkError e) {
            OPFLog.w(String.format(US, "Message %s hasn't been delivered. It'll be resend later.", message));

            //Save undelivered message to shared preferences
            final String newUndeliveredMessageKey = String.valueOf(System.currentTimeMillis());
            final OPFPreferences preferences = new OPFPreferences(context, OPF_UNITY_POSTFIX);
            final Set<String> undeliveredMessagesKeys = preferences.getStringSet(UNDELIVERED_MESSAGES_KEYS, new HashSet<String>());
            undeliveredMessagesKeys.add(newUndeliveredMessageKey);
            preferences.put(UNDELIVERED_MESSAGES_KEYS, undeliveredMessagesKeys);
            preferences.put(newUndeliveredMessageKey + UNDELIVERED_MESSAGE_CALLBACK_POSTFIX, callbackMethod);
            preferences.put(newUndeliveredMessageKey + UNDELIVERED_MESSAGE_BODY_POSTFIX, message);
        }
    }

    @SuppressWarnings("unused")
    public static void resendUndeliveredMessages() {
        OPFLog.logMethod();
        final Context context = UnityPlayer.currentActivity;
        final OPFPreferences preferences = new OPFPreferences(context, OPF_UNITY_POSTFIX);
        final Set<String> undeliveredMessagesKeys = new TreeSet<>(new Comparator<String>() {
            @Override
            public int compare(final String lhs, final String rhs) {
                //Keys are string representation of System.currentTimeMillis();
                final long left = Long.parseLong(lhs);
                final long right = Long.parseLong(rhs);

                return (int) (left - right);
            }
        });
        undeliveredMessagesKeys.addAll(preferences.getStringSet(UNDELIVERED_MESSAGES_KEYS, new HashSet<String>(0)));
        preferences.remove(UNDELIVERED_MESSAGES_KEYS);

        for (String key : undeliveredMessagesKeys) {
            final String callbackMethodKey = key + UNDELIVERED_MESSAGE_CALLBACK_POSTFIX;
            final String messageKey = key + UNDELIVERED_MESSAGE_BODY_POSTFIX;
            final String callbackMethod = preferences.getString(callbackMethodKey);
            final String message = preferences.getString(messageKey);

            preferences.remove(callbackMethodKey);
            preferences.remove(messageKey);

            if (callbackMethod != null && message != null) {
                sendUnityMessage(context, callbackMethod, message);
            }
        }
    }
}
