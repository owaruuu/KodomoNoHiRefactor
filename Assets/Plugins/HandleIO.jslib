var HandleIO = {
    WindowAlert : function(message)
    {
        window.alert(Pointer_stringify(message));
    },
    SyncFiles : function()
    {
        FS.syncfs(false,function (err) {
        });
    }
};

mergeInto(LibraryManager.library, HandleIO);