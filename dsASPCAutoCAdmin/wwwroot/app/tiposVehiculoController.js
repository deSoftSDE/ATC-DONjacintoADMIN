appadmin.controller('TiposVehiculo', function ($scope, Llamada, $timeout) {
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
        editing: {
            allowAdding: false, // Enables insertion
            allowDeleting: false, // Enables removing
            editEnabled: false
        },
        selection: {
            mode: "single"
        },
        masterDetail: {
            enabled: false,
            template: "detail"
        },
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
            console.log(e);
            alert("abierto");
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
            }, {
                caption: "",
                width: 100,
                allowFiltering: false,
                allowSorting: false,
                allowEditing: false,
                cellTemplate: "editTemplate"
            }, {
                caption: "",
                width: 80,
                allowFiltering: false,
                allowSorting: false,
                allowEditing: false,
                cellTemplate: "deleteTemplate"
            }
        ],
        onInitialized: function (e) {
            console.log(e);
            $scope.datagrid = e.component;
        },
        onRowUpdated: function (e) {
            alert("Hola");
            console.log(e);
        },
        onRowUpdating: function (e) {
            alert("Hola");
            console.log(e);
        },
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
    $scope.guardarCambios = function (vehiculo) {
        if (NotNullNotUndefinedNotEmpty(vehiculo.files)) {
            console.log(document.getElementById("filesup").files);
            var fd = new FormData();
            fd.append('file.jpg', document.getElementById("filesup").files[0]);
            console.log(fd);
            console.log(vehiculo.files);
            //console.log($scope.files);
            vehiculo.url = vehiculo.files.data;
            //console.log(vidrio.files.data);
            Llamada.postFile(fd)
                .then(function (respuesta) {
                    vehiculo.imagen = respuesta[0].contenido;
                    document.getElementById("filesup").value = null;
                    guardarCambios(vehiculo);
                });
        } else {
            vehiculo.newImagen = vehiculo.imagen;
            guardarCambios(vehiculo);
        }

        $scope.datagrid.collapseAll(-1);
    };
    $scope.modificarTipoVehiculo = function (vid) {
        $scope.popupVisible = true;
        $scope.currentvehiculo = vid.data;
    };
    guardarCambios = function (vehiculo) {
        vehiculo.files = null;
        Llamada.post("TiposVehiculoCrearModificar", vehiculo)
            .then(function (respuesta) {
                mensajeExito("Datos guardados con éxito");
                if (ZeroSiNull(vehiculo.idTipoVehiculo) < 1) {
                    var obj = {
                        tipo: "TiposVehiculo",
                        cadena: "",
                        accionPagina: "N",
                        lastValor: vehiculo.descripcion,
                        lastIndice: respuesta.data.identidad
                    };
                    vehiculo.idTipoVehiculo = respuesta.identidad;
                    LeerRegistros(obj, vehiculo);
                }
                console.log(respuesta);

                $scope.popupVisible = false;
            });

    };
    $scope.popupVisible = false;
    $scope.popupOptions = {
        width: 660,
        height: "auto",
        showTitle: true,
        title: "Editar/crear Tipo de Vehículo",
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
        Llamada.get("TipoVehiculoEliminar?idTipoVehiculo=" + a.data.idTipoVehiculo)
            .then(function (respuesta) {
                console.log(respuesta);
                LeerRegistros($scope.lastConsulta);
            });
    };
    $scope.hola = function () {
        alert("Hola");
    };
    var obj = {
        tipo: "TipoVehiculo",
        cadena: ""
    };
    LeerRegistros(obj);
    $scope.currentcarroceria = {
        descripcion: "Descripción"
    };
    $scope.crearRegistro = function () {
        $scope.popupVisible = true;
        $scope.currentvehiculo = {
            descripcion: "Descripción"
        };
    };
    $scope.guardarCambiosPopup = function () {
        $scope.guardarCambios($scope.currentvehiculo);
    };
    $scope.cancelarCambios = function () {
        $scope.popupVisible = false;
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
    fnocultar = function () {
        $scope.mostrardesplegable = false;
    }
    $scope.focofuera = function (a, b) {
        $timeout(fnocultar, 1000);
    }
    $scope.entrafoco = function () {
        if (NotNullNotUndefinedNotEmpty($scope.resultadobusqueda)) {
            $scope.mostrardesplegable = true;
        }
    }
    buscarArticulos = function () {
        $scope.loading = true;
        if ($scope.cadena.length > 0) {
            Llamada.get("TiposVidrioLeerPorCadena?cadena=" + $scope.cadena)
                .then(function (respuesta) {
                    $scope.resultadobusqueda = respuesta.data;
                    //$scope.NumReg = respuesta.data.numReg;
                    $scope.mostrardesplegable = true;
                    $scope.loading = false;
                    document.getElementById("desplegable").style.display = "inline";
                })
        } else {
            $scope.resultadobusqueda = [];
            $scope.NumReg = 0;
            $scope.loading = false;
        }
    }
    var inputChangedPromise;
    $scope.cambiobusqueda = function () {
        if (inputChangedPromise) {
            $timeout.cancel(inputChangedPromise);
        }
        inputChangedPromise = $timeout(buscarArticulos, 1000);
    }
    $scope.anadirVidrio = function (res) {
        console.log(res);
        $scope.mostrardesplegable = false;
        if (!NotNullNotUndefinedNotEmpty($scope.currentcarroceria.vidrios)) {
            $scope.currentcarroceria.vidrios = [];
        }
        var vid = {
            IDVidrio: 0,
            Imagen: res.imagen,
            descripcion: res.descripcion,
            IdTipoVidrio: res.idTipoVidrio,
            modificador: 1
        }
        $scope.currentcarroceria.vidrios.push(vid);
    }
    $scope.noEliminado = function (vidrio) {
        if (NotNullNotUndefinedNotEmpty(vidrio)) {
            if (NotNullNotUndefinedNotEmpty(vidrio.modificador)) {
                if (vidrio.modificador > 1) {
                    return false;
                } else {
                    return true;
                }
            } else {
                return true;
            }
        } else {
            return false;
        }
    }
    $scope.eliminarVidrio = function (vidrio, $index) {
        result = DevExpress.ui.dialog.confirm("¿Seguro que deseas eliminar este vidrio?");
        result.then(function (val) {
            if (val) {
                console.log("OK");
                $scope.$apply(function () {
                    vidrio.modificador = 2;
                });


                console.log(vidrio);
            }
        });
    }
});