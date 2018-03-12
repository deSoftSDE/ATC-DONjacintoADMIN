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
            allowDeleting: true, // Enables removing
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
            console.log("HOLIII");
            info.component.saveEditData();
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
                width: 100,
                caption: "IDTipoVidrio"
            }, {
                dataField: "url",
                caption: "Imagen",
                width: 150,
                allowFiltering: false,
                allowSorting: false,
                allowEditing: false,
                cellTemplate: "cellTemplate"
            }, {
                dataField: "descripcion",
                width: 600,
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
        }
    };
    LeerRegistros = function (obj) {
        $scope.lastConsulta = JSON.parse("" + JSON.stringify(obj));
        Llamada.post("LecturasGenericasPaginadas", obj)
            .then(function (respuesta) {
                if (respuesta.data.articulos.length < 1) {
                    switch (val) {
                        case "N":
                            cambiarBotonesPaginacionFinales("disabled");
                            break;
                        case "P":
                            cambiarBotonesPaginacionIniciales("disabled");
                            break;
                    }

                } else {
                    $scope.vm = respuesta.data;
                    $scope.orders = respuesta.data.articulos;
                    for (i = 0; i < $scope.orders.length; i++) {
                        $scope.orders[i].url = Llamada.getRuta($scope.orders[i].imagen);
                    }
                    console.log($scope.orders);
                    console.log("Arriba las orders");
                    $scope.datagrid.option("dataSource", $scope.orders);
                }
                //$scope.datagrid.repaint();
            });
    };
    
    $scope.files = "";
    $scope.guardarCambios = function (vidrio) {
        console.log("Aqui llegae l vidrio");
        console.log(vidrio);
        if (NotNullNotUndefinedNotEmpty(vidrio.files)) {
            console.log(document.getElementById("filesup").files);
            console.log(vidrio);
            var fd = new FormData();
            fd.append('file.jpg', document.getElementById("filesup").files[0]);
            console.log(fd);
            console.log(vidrio.files);
            //console.log($scope.files);
            vidrio.url = vidrio.files.data;
            //console.log(vidrio.files.data);
            Llamada.postFile(fd)
                .then(function (respuesta) {
                    vidrio.imagen = respuesta[0].contenido;
                    document.getElementById("filesup").files = null;
                    guardarCambios(vidrio);
                });
        } else {
            vidrio.newImagen = vidrio.imagen;
            guardarCambios(vidrio);
        }

        $scope.datagrid.collapseAll(-1);
    };
    $scope.modificarVidrio = function (vid) {
        $scope.popupVisible = true;
        $scope.currentvidrio = vid.data;
    };
    guardarCambios = function (vidrio) {
        console.log("Guardando...");
        console.log(vidrio);
        vidrio.files = null;
        Llamada.post("TiposVidrioCrearModificar", vidrio)
            .then(function (respuesta) {
                mensajeExito("Datos guardados con éxito");
                $scope.popupVisible = false;
            });

    };
    $scope.popupVisible = false;
    $scope.popupOptions = {
        width: 660,
        height: 540,
        showTitle: true,
        title: "Nuevo Tipo de Vidrio",
        dragEnabled: false,
        bindingOptions: {
            visible: 'popupVisible'
        },
        closeOnOutsideClick: true
    };
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
    var obj = {
        tipo: "TiposVidrio",
        cadena: ""
    };
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
        console.log($scope.currentvidrio);
        $scope.guardarCambios($scope.currentvidrio);
        console.log($scope.currentvidrio);
    }
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
});
