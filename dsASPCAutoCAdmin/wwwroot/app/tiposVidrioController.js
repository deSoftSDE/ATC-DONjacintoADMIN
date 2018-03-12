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
        keyExpr: "id",
        editing: {
            allowAdding: false, // Enables insertion
            allowDeleting: false, // Enables removing
            editEnabled: false
        },
        selection: {
            mode: "single"
        },
        /*masterDetail: {
            enabled: false,
            template: "detail"
        },*/
        onRowInserted: function (info, a) {
            console.log(info);
            console.log(a);
        },
        onInitNewRow: function (info, b) {
            console.log("init");
            console.log(info);
            info.data.descripcion = 'Nuevo';
            console.log("HOLIII")
            info.component.saveEditData()
        },
        /*onSelectionChanged: function (e) {
            e.component.collapseAll(-1);
            e.component.expandRow(e.currentSelectedRowKeys[0]);
        },*/
        onRowRemoving: function (e) {
            console.log(e);
            console.log(e.data.idTipoVidrio);
            eliminarRegistro(e.data.idTipoVidrio);
        },
        columns: [
            {
                dataField: "idTipoVidrio",
                allowEditing: false,
                width: 130,
                caption: "IDTipoVidrio"
            }, {
                dataField: "imagen",
                caption: "Imagen",
                width: 100,
                allowFiltering: false,
                allowSorting: false,
                allowEditing: false,
                cellTemplate: "cellTemplate"
            }, {
                dataField: "descripcion",
                width: 130,
                caption: "Descripcion"
            },
            /*{
                caption: "Eliminar",
                dataField: "idTipoVidrio",
                width: 100,
                allowFiltering: false,
                allowSorting: false,
                allowEditing: false,
                cellTemplate: function (container, options) {
                    $('<div />').dxButton({
                        icon: 'trash',
                        type: 'danger',
                        onClick: function (e) {
                            $('#gridContainer').dxDataGrid('instance').deleteRow(options.rowIndex);
                        }
                    }).appendTo(container);
                }
            },*/
            {
                caption: "Modificar",
                width: 100,
                allowFiltering: false,
                allowSorting: false,
                allowEditing: false,
                cellTemplate: "editTemplate"
            }
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
        },
        rowUpdating: function (e) {
            alert("Ey")
            console.log(e);
        },
        rowUpdated : function (e) {
            alert("Ey")
            console.log(e);
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
    };
    var obj = {
        tipo: "TiposVidrio",
        cadena: ""
    };
    $scope.files = "";
    $scope.guardarCambios = function (vidrio) {
        if (NotNullNotUndefinedNotEmpty(vidrio.files)) {
            console.log(vidrio);
            var fd = new FormData();
            fd.append('file.jpg', vidrio.files);
            console.log(fd);
            console.log($scope.files);
            vidrio.imagen = vidrio.files.data;
            Llamada.postFile(fd)
                .then(function (respuesta) {
                    vidrio.newImagen = respuesta.contenido;
                    guardarCambios(vidrio);
                });
        } else {
            vidrio.newImagen = vidrio.imagen;
            guardarCambios(vidrio);
        }

        $scope.datagrid.collapseAll(-1);
    };
    $scope.modificarVidrio = function (vid) {
        alert("Holi");
        console.log(vid);
        $scope.popupVisible = true;
        $scope.currentvidrio = vid.data;
    }
    guardarCambios = function (vidrio) {
        Llamada.post("TiposVidrioCrearModificar", vidrio)
            .then(function (respuesta) {
                $scope.datagrid.collapseAll(-1);
                mensajeExito("Datos guardados con éxito");
            });

    };
    $scope.popupVisible = false;
    $scope.popupOptions = {
        width: 660,
            height: 540,
                showTitle: true,
                    dragEnabled: false,
                        bindingOptions: {
                            visible: 'popupVisible'
                        },
        closeOnOutsideClick: true,
    }
    $scope.cambioInput = function () {
        alert("Holi");
    };
    eliminarRegistro = function (id) {
        Llamada.get("TiposVidrioEliminar?idTipoVidrio=" + id)
            .then(function (respuesta) {
                console.log(respuesta);
            });
    };
    $scope.hola = function () {
        alert("Hola");
    }
    LeerRegistros(obj);
    $scope.currentvidrio = {
        descripcion: "Descripción",
    };
    $scope.crearRegistro = function () {
        $scope.popupVisible = true;
        $scope.currentvidrio = {
            descripcion: "Descripción",
        };
    };
    $scope.guardarCambiosPopup = function () {
        alert("Ok");
        console.log($scope.currentvidrio);
    }
});
