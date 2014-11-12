package org.onepf.opfpush.unity;

import org.onepf.opfpush.Options;

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
        SharedPreferences settings = getSharedPreferences(PREFS_NAME, 0);
        SharedPreferences.Editor editor = settings.edit();
        editor.putBoolean("silentMode", mSilentMode);

        // Commit the edits!
        editor.commit();
    }
    
    Options loadOptions() {
        return null;
    }
    
    public void init(Options options) {
    }
 }
