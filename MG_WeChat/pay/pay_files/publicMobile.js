(function (doc, win) {    
    var docEl = doc.documentElement,    
    resizeEvt = 'orientationchange' in window ? 'orientationchange' : 'resize',    
    recalc = function () {    
    var clientWidth = docEl.clientWidth;  
    if (!clientWidth) return;
    // var _newFontSize = 20 * ( clientWidth / 320 );
    //控制_newFontSize的范围，让html的最大宽度在320和640之间
    // if(_newFontSize > 40){
    //     _newFontSize = 40;
    // }else if(_newFontSize < 20){
    //     _newFontSize =20;
    // }

    docEl.style.fontSize = 20 * ( clientWidth / 320 ) + 'px'; 
    };    
	if (!doc.addEventListener) return;    
	win.addEventListener(resizeEvt, recalc, false);    
	doc.addEventListener('DOMContentLoaded', recalc, false);    
})(document, window);
