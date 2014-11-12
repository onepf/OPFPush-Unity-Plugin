package org.onepf.opfpush.unity;

import android.app.Activity;
import android.util.Log;
import java.io.File;

public class CustomApplication extends android.app.Application 
{
    private final String TAG = "OPFPush-Unity";
	
    @Override
    public void onCreate() {
        super.onCreate();
		Log.d(TAG, "Application.onCreate");
	}
}
