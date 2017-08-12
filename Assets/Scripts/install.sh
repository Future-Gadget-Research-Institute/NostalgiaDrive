echo 'Downloading from http://download.unity3d.com/download_unity/a2913c821e27/MacEditorInstaller/Unity-5.6.2f1.pkg?_ga=2.1274289.1479801571.1502550060-379507740.1499466665: '
curl -o Unity.pkg http://download.unity3d.com/download_unity/a2913c821e27/MacEditorInstaller/Unity-5.6.2f1.pkg?_ga=2.1274289.1479801571.1502550060-379507740.1499466665
echo 'Installing Unity.pkg’
sudo installer -dumplog -package Unity.pkg -target /
