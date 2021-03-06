var saveAs = require('FileSaver.js/FileSaver').saveAs;
var utils = require('./utils');
var jsPDF = require('./jspdf');

module.exports = function (table, filename) {
    var pdf = new jsPDF('p','pt', 'a4', false);
    var startColPosition = 20;
    var startRowPosition = 20;
    var colPosition = 0;
    var rowPosition = 0;
    var page = 1;
    var save = function (pdf) {
        saveAs(pdf.output('blob'), filename + '.pdf');
    };
    pdf.setFontSize(14);

    if (/[\u4E00-\u9FA5]|[\uFE30-\uFFA0]/gi.test(utils.getText(table))) {
        pdf.addDOM(table, startRowPosition, startColPosition, function () {
            save(pdf);
        });
    } else {
        for (var i = 0, row; row = table.rows[i]; i++) {
            if ((i + 1) % 26 === 0){
                pdf.addPage();
                page++;
                startRowPosition = startRowPosition + 10;
            }
            rowPosition = startRowPosition + ((i + 1) * 10) - ((page -1) * 280);
            for (var j = 0, col; col = row.cells[j]; j++) {
                colPosition = startColPosition + (j * 50);
                pdf.text(utils.getText(col), colPosition, rowPosition);
            }
        }
        save(pdf);
    }
}