// create container needed for methods below
var redips = {};

// initialization
redips.init = function () {
    // reference to the REDIPS.drag library
    var rd = REDIPS.drag;
    // initialization
    rd.init();
    // set hover color
    rd.hover.colorTd = '#9BB3DA';
    // event handler invoked before DIV element is dropped to the cell
    rd.event.droppedBefore = function (targetCell) {
        // test if target cell is occupied and set reference to the dragged DIV element
        var empty = rd.emptyCell(targetCell, 'test'),
            obj = rd.obj;
        // if target cell is not empty
        if (!empty) {
            // confirm question should be wrapped in setTimeout because of
            // removeChild and return false below
            setTimeout(function () {
                // ask user if he wants to overwrite TD (cell is already occupied)
                if (confirm('Overwrite content?')) {
                    rd.emptyCell(targetCell);
                }
                // append previously removed DIV to the target cell
                targetCell.appendChild(obj);
            }, 50);
            // remove dragged DIV from from DOM (node still exists in memory)
            obj.parentNode.removeChild(obj);
            // return false (deleted DIV will not be returned to source cell)
            return false;
        }
    };
}


// add onload event listener
if (window.addEventListener) {
    window.addEventListener('load', redips.init, false);
}
else if (window.attachEvent) {
    window.attachEvent('onload', redips.init);
}
