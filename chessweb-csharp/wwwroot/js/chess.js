window.chessInterop = {
    setData: function (data) {
        window.dragData = data;
    },
    getData: function () {
        return window.dragData;
    }
};