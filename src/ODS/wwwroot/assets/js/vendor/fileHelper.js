window.tableToExcel = (function () {
    var uri = 'data:application/vnd.ms-excel;base64,'
        , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head> <!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--><meta http-equiv="content-type" content="text/plain; charset=UTF-8"/><style>{cssStyle}</style></head><body><table>{table}</table></body></html>'
        , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
        , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
    return function (table, name, fileName) {
        if (!table.nodeType) table = document.getElementById(table)
        var xmlhttp;
        xmlhttp = new XMLHttpRequest();
        xmlhttp.addEventListener("load", done, false);
        xmlhttp.open("GET", "assets/css/excel-css.css", true);
        xmlhttp.send();
        function done() {
            var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML, cssStyle: xmlhttp.responseText }

            // window.location.href = uri + base64(format(template, ctx))
            anchor = document.createElement("a");
            anchor.href = uri + base64(format(template, ctx));
            anchor.download = fileName;
            anchor.click();
            anchor.remove();

        }
    }
})()