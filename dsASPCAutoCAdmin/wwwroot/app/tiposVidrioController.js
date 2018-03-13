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
            editEnabled: false,
            texts: {
                deleteRow:"eliminar",
            }
        },
        width: "auto",
        columnAutoWidth:true,
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
        columns: [
            {
                dataField: "url",
                caption: "Imagen",
                width: 100,
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
                caption: "",
                width: 80,
                allowFiltering: false,
                allowSorting: false,
                allowEditing: false,
                cellTemplate: "editTemplate"
            },
            {
                caption: "",
                width: 80,
                allowFiltering: false,
                allowSorting: false,
                allowEditing: false,
                cellTemplate: "deleteTemplate"
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
    LeerRegistros = function (obj, objmodificado) {
        $scope.lastConsulta = JSON.parse("" + JSON.stringify(obj));
        Llamada.post("LecturasGenericasPaginadas", obj)
            .then(function (respuesta) {
                if (respuesta.data.articulos.length < 1) {
                    switch (obj.accionPagina) {
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
                    if (NotNullNotUndefinedNotEmpty(objmodificado)) {
                        $scope.orders.splice(0, 0, objmodificado);
                    }
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
                if (ZeroSiNull(vidrio.idTipoVidrio) < 1) {
                    var obj = {
                        tipo: "TiposVidrio",
                        cadena: "",
                        accionPagina: "N",
                        lastValor: vidrio.descripcion,
                        lastIndice: respuesta.data.identidad
                    };
                    vidrio.idTipoVidrio = respuesta.identidad;
                    LeerRegistros(obj, vidrio);
                }
                console.log(respuesta);
                
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
    $scope.eliminarRegistro = function (a) {
        console.log(a);
        result = DevExpress.ui.dialog.confirm("¿Seguro que deseas eliminar este tipo de vidrio?");
        result.then(function (val) {
            if (val) {
                Llamada.get("TiposVidrioEliminar?idTipoVidrio=" + a.data.idTipoVidrio)
                    .then(function (respuesta) {
                        console.log(respuesta);
                            LeerRegistros($scope.lastConsulta);
                    });
            }
        });
        
    };
    $scope.hola = function () {
        alert("Hola");
    };
    var obj = {
        tipo: "TiposVidrio",
        cadena: ""
    };
    LeerRegistros(obj);
    $scope.currentvidrio = {
        descripcion: "Descripción"
    };
    $scope.crearRegistro = function () {
        $scope.popupVisible = true;
        $scope.currentvidrio = {
            descripcion: "Descripción"
        };
    };
    $scope.guardarCambiosPopup = function () {
        console.log($scope.currentvidrio);
        $scope.guardarCambios($scope.currentvidrio);
        console.log($scope.currentvidrio);
    };
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
    $scope.cancelarCambios = function () {
        $scope.popupVisible = false;
    }
});
