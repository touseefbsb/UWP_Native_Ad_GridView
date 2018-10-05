# UWP_Native_Ad_GridView_Bug
native ad in gridview causes a bug on top of the app UI

> Steps To Reproduce.

1. Run the app on x86 architecture.
2. From left navigation pane select "Library".
3. you will see a GridView Where app will list folder in your "KnownFolders.VideoLibrary" then there will be 1 "NativeAd" gridviewitem with test native ad showing sponsored tag on it as well. At last you will see all video files in your "KnownFolders.VideoLibrary".
4. Now click on History from left Navigation Pane the app will go to history page which is basically empty. But you will notice a sponsored tag right at top of the app ( below title bar ). You cannot click it because the app UI is extended into title bar, but if you stop extending the app into the title bar you will notice that extra sponsored tag actually appeard on top of whole app UI and is actually clickable ( if app not extended into title bar).


> Here is attached image and you can notice the extra sponsored tag at the top.


> And Here is image of "Live Visual Tree Helper" which actually indicates that whole app is somehow selected as clickable target, that is why sponsored tag seems to appear at top. Though notice that clicking anywhere in app doesnt invoke the native Ad, because the app never tried to assign complete App UI as clickable native Ad.


Also note that if you have sub folders you can click folder icon nd they will also open on Library page, and then again moving away from library page onto history page causes another sponsored tag to appear on top, so repeating the process again and again causes more and more sponsored tags to appear on top towards top left side.

Testing windows 10 device has version 1803 ( april 2018 update ) and VS 2017 15.8.6
