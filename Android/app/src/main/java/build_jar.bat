echo "Building CustomApplication..."
javac CustomApplication.java -bootclasspath %ANDROID_HOME%/platforms/android-8/android.jar -d .

echo "Signature dump of CustomApplication..."

javap -s org.onepf.opfpush.unity.CustomApplication
javap -s org.onepf.opfpush.unity.PushHelper

echo "Creating JavaClass.jar..."
jar cvfM ../OPFPush-plugin.jar org/

echo "Cleaning up / removing build folders..."  #optional..
rm -rf libs
rm -rf obj
rm -rf org

echo "Done!"
