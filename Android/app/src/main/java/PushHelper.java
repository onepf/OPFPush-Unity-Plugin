package org.onepf.opfpush.unity;

import android.content.SharedPreferences;

import org.onepf.opfpush.Options;
import org.onepf.opfpush.ExponentialBackoff;

public class PushHelper extends android.app.Application 
{
    private static final String TAG = "OPFPush-Unity";
	private static final String PREFS = "OPFPush-Unity-PREFS";
    
    private static PushHelper _instance;
    public static PushHelper instance() {
        if (_instance == null) {
            _instance = new PushHelper();
        }
        return _instance;
    }
    
    void saveOptions(Options options) {
        SharedPreferences settings = getSharedPreferences(PREFS, 0);
        SharedPreferences.Editor editor = settings.edit();

        editor.putBoolean("recoverProvider", options.isRecoverProvider());
        editor.putBoolean("selectSystemPreferred", options.isSelectSystemPreferred());

        editor.commit();
    }
    
    Options loadOptions() {
        SharedPreferences settings = getSharedPreferences(PREFS, 0);
        boolean recoverProvider = settings.getBoolean("recoverProvider", true);
        boolean selectSystemPreferred = settings.getBoolean("selectSystemPreferred", true);

        Options.Builder builder = new Options.Builder();
        //builder.addProviders(new GCMProvider(this, GCM_SENDER_ID));
        builder.setRecoverProvider(recoverProvider)
               .setSelectSystemPreferred(selectSystemPreferred)
               .setBackoff(new ExponentialBackoff(Integer.MAX_VALUE));

        return builder.build();
    }
    
    public void init(Options options) {
        saveOptions(options);

    }
 }
