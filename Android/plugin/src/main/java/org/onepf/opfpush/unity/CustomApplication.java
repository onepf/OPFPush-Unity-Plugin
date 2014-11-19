package org.onepf.opfpush.unity;

import android.app.Activity;
import android.util.Log;
import java.io.File;
import org.onepf.opfpush.ExponentialBackoff;
import org.onepf.opfpush.OPFPushHelper;
import org.onepf.opfpush.OPFPushLog;
import org.onepf.opfpush.Options;
import org.onepf.opfpush.gcm.GCMProvider;


public class CustomApplication extends android.app.Application 
{
    private final String TAG = "OPFPush-Unity";
	
    @Override
    public void onCreate() {
        super.onCreate();
		Log.d(TAG, "Application.onCreate");

        //OPFPushHelper.getInstance(this).init(options);
	}
}
