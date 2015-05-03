
NAME=hak

adb
adb devices
adb shell pm
adb shell pm list packages


adb shell pm list packages | grep $NAME

PACKAGE=com.infinum.hak

adb shell pm path $PACKAGE

PACKAGE_PATH=/data/app/com.infinum.hak-1/base.apk
DESTINATION_PATH=~/Projects/Samples/Samples.Xamarin.UITest/

adb pull $PACKAGE_PATH $DESTINATION_PATH