echo 'Downloading from http://download.unity3d.com/download_unity/a2913c821e27/UnityDownloadAssistant-5.6.2f1.dmg: '
curl -o Unity.dmg http://download.unity3d.com/download_unity/a2913c821e27/UnityDownloadAssistant-5.6.2f1.dmg
echo 'Installing Unity.dmg’
sudo installer -dumplog -package Unity.dmg -target /