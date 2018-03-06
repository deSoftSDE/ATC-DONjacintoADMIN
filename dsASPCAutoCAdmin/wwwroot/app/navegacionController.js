﻿appadmin.controller('navegacion', function ($scope) {
    console.log("Holi");
    obtenerImagenInput = function (imagen) {
        var reader = new FileReader();
        var a = window.URL.createObjectURL(imagen);
        return a;
    };
    cambiarBotonesPaginacion = function (val) {
        console.log("Desactivando");
        document.getElementById("primera").className = val;
        document.getElementById("anterior").className = val;
        document.getElementById("siguiente").className = val;
        document.getElementById("ultima").className = val;
    };
    cambiarBotonesPaginacionIniciales = function (val) {
        console.log("Desactivando");
        document.getElementById("primera").className = val;
        document.getElementById("anterior").className = val;
    };
    cambiarBotonesPaginacionFinales = function (val) {
        console.log("Desactivando");
        document.getElementById("siguiente").className = val;
        document.getElementById("ultima").className = val;
    };
    loopClase = function (clase) {
        var elem = document.getElementsByClassName(clase);
        for (i = 0; i < elem.length; i++) {
            console.log(elem[i]);
            elem[i].setAttribute("style", "");
        }
        document.getElementById("loading" + clase).style.display = "none";
    };
});

appadmin.controller('TiposVidrio', function ($scope, Llamada) {
    console.log("Holi");
    
    
    

    $scope.cambiarPagina = function (sender, val) {
        cambiarBotonesPaginacion("");
        switch (val) {
            case "F":
                cambiarBotonesPaginacionIniciales("disabled");
                break;
            case "L":
                cambiarBotonesPaginacionFinales("disabled");
                break;
        }
        $scope.vm.cm.accionPagina = val;
        LeerRegistros($scope.vm.cm);
    };


    

    
    $scope.dataGridOptions = {
        dataSource: [],
        keyExpr: "idTipoVidrio",
        allowAdding: true,
        selection: {
            mode: "single"
        },
        masterDetail: {
            enabled: true,
            template: "detail"
        },
        onSelectionChanged: function (e) {
            e.component.collapseAll(-1);
            e.component.expandRow(e.currentSelectedRowKeys[0]);
        },
        columns: [
            {
                dataField: "imagen",
                caption: "Imagen",
                width: 100,
                allowFiltering: false,
                allowSorting: false,
                cellTemplate: "cellTemplate"
            }, {
                dataField: "idTipoVidrio",
                width: 130,
                caption: "IDTipoVidrio"
            }, {
                dataField: "descripcion",
                width: 130,
                caption: "Descripcion"
            }, 
        ],
        summary: {
            totalItems: [{
                column: "OrderNumber",
                summaryType: "count"
            }, {
                column: "OrderDate",
                summaryType: "min",
                customizeText: function (data) {
                    return "First: " + Globalize.formatDate(data.value, { date: "medium" });
                }
            }, {
                column: "SaleAmount",
                summaryType: "sum",
                valueFormat: "currency"
            }]
        },
        onInitialized: function (e) {
            console.log(e);
            $scope.datagrid = e.component;
        }
    };
    LeerRegistros = function (obj) {
        Llamada.post("EjemploTipoVidrio", obj)
            .then(function (respuesta) {
                $scope.vm = respuesta.data;
                $scope.orders = respuesta.data;
                console.log($scope.orders);
                console.log("Arriba las orders");
                $scope.datagrid.option("dataSource", respuesta.data);
                //$scope.datagrid.repaint();
            });
    }
    var obj = {
        tipo: "TiposVidrio",
        cadena: ""
    }
    $scope.files = "";
    $scope.guardarCambios = function (vidrio) {
        console.log(vidrio);
        var fd = new FormData();
        fd.append('file.jpg', vidrio.files);
        console.log(fd);
        console.log($scope.files)
        vidrio.imagen = vidrio.files.data;
        $scope.datagrid.collapseAll(-1);
    }
    $scope.cambioInput = function () {
        alert("Holi");
    }
    LeerRegistros(obj);
});
