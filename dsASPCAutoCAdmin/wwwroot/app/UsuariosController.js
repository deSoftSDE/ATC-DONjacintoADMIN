appadmin.controller('Usuarios', function ($scope, Llamada, $timeout) {
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
    $scope.showColumnLines = false;
    $scope.showRowLines = true;
    $scope.showBorders = true;
    $scope.rowAlternationEnabled = true;
    $scope.dataGridOptions = {
        bindingOptions: {
            showColumnLines: "showColumnLines",
            showRowLines: "showRowLines",
            showBorders: "showBorders",
            rowAlternationEnabled: "rowAlternationEnabled"
        },
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
                dataField: "nombre",
                width: "30%",
                caption: "Usuario"
            }, {
                width: "20%",
                allowFiltering: false,
                allowSorting: false,
                allowEditing: false,
                alignment: "center",
                caption: "Empresa",
                cellTemplate: "editTemplate"
            }, {
                width: "20%",
                dataField: "activo",
                caption: "Activo"
            }, {
                width: "20%",
                dataField: "ultimoAcceso",
                dataType: 'date',
                format: "MM/dd/yyyy HH:mm",
                caption: "Último acceso"
            }, {
                width: "20%",
                dataField: "ipAddress",
                caption: "Dirección IP"
            }, {
                caption: "",
                width: "20%",
                allowFiltering: false,
                allowSorting: false,
                allowEditing: false,
                alingment: "center",
                cellTemplate: "deleteTemplate",
                caption: "Eliminar"
            }, {
                caption: "",
                width: "20%",
                allowFiltering: false,
                allowSorting: false,
                allowEditing: false,
                alingment: "center",
                cellTemplate: "permisosTemplate",
                caption:"Permisos"
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
        }
    };
    $scope.dataGridClientes = {
        bindingOptions: {
            showColumnLines: "showColumnLines",
            showRowLines: "showRowLines",
            showBorders: "showBorders",
            rowAlternationEnabled: "rowAlternationEnabled"
        },
        dataSource: [],
        editing: {
            allowAdding: false, // Enables insertion
            allowDeleting: false, // Enables removing
            editEnabled: false
        },
        selection: {
            mode: "single"
        },
        columns: [
            {
                dataField: "nombre",
                width: "30%",
                caption: "Cliente"
            }, {
                dataField: "delegacion",
                width: "30%",
                caption: "Delegacion"
            }, {
                dataField: "nombreMunicipio",
                width: "30%",
                caption: "Municipio"
            }, {
                dataField: "nombreProvincia",
                width: "30%",
                caption: "Provincia"
            }, {
                width: "20%",
                allowFiltering: false,
                allowSorting: false,
                allowEditing: false,
                alignment: "center",
                caption: "Invitar Cliente",
                cellTemplate: "editTemplate"
            }
        ],
        onInitialized: function (e) {
            console.log(e);

            //alert("Inicializado");
            $scope.datagridclientes1 = e.component;
            var c = {
                pagina: 1,
                bloque: 6
            };
            LeerClientes(c);
        },
        onRowUpdated: function (e) {
            alert("Hola");
            console.log(e);
        },
        onRowUpdating: function (e) {
            alert("Hola");
            console.log(e);
        }
    };
    $scope.dataGridClientes2 = {
        bindingOptions: {
            showColumnLines: "showColumnLines",
            showRowLines: "showRowLines",
            showBorders: "showBorders",
            rowAlternationEnabled: "rowAlternationEnabled"
        },
        dataSource: [],
        editing: {
            allowAdding: false, // Enables insertion
            allowDeleting: false, // Enables removing
            editEnabled: false
        },
        selection: {
            mode: "single"
        },
        columns: [
            {
                dataField: "nombre",
                width: "30%",
                caption: "Cliente"
            }, {
                dataField: "delegacion",
                width: "30%",
                caption: "Delegacion"
            }, {
                dataField: "nombreMunicipio",
                width: "30%",
                caption: "Municipio"
            }, {
                dataField: "nombreProvincia",
                width: "30%",
                caption: "Provincia"
            }, {
                width: "20%",
                allowFiltering: false,
                allowSorting: false,
                allowEditing: false,
                alignment: "center",
                caption: "Seleccionar",
                cellTemplate: "editTemplate"
            }
        ],
        onInitialized: function (e) {
            console.log(e);

            //alert("Inicializado");
            $scope.datagridclientes2 = e.component;
            var c = {
                pagina: 1,
                bloque: 6
            };
            LeerClientes(c);
        },
        onRowUpdated: function (e) {
            alert("Hola");
            console.log(e);
        },
        onRowUpdating: function (e) {
            alert("Hola");
            console.log(e);
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
    
    LeerClientes = function (obj) {
        Llamada.post("ClientesLeer", obj)
            .then(function (respuesta) {
                //alert("HOLIIII");
                try {
                    $scope.datagridclientes1.option("dataSource", respuesta.data.clientes);
                }
                catch (ex) {
                    //alert("Cazado en 1");
                    //console.log(ex);
                }
                try {
                    $scope.datagridclientes2.option("dataSource", respuesta.data.clientes);
                }
                catch (ex) {
                    //alert("Cazado en 2");
                    //console.log(ex);
                }
                //$scope.datagridclientes2.option("dataSource", respuesta.data.clientes);
                $scope.pagina = respuesta.data.busqueda.pagina;
                $scope.bloque = respuesta.data.busqueda.bloque;
                $scope.registros = respuesta.data.numReg;
                if (NotNullNotUndefinedNotEmpty(respuesta.data.busqueda.nCliente)) {
                    $scope.nCliente = respuesta.data.busqueda.nCliente;
                } else {
                    $scope.nCliente = "";
                }

            });
    };

    LeerUltimosClientes = function () {
        var obj = {
            pagina: $scope.pagina,
            bloque: $scope.bloque,
            nCliente: $scope.ncliente
        };
        LeerClientes(obj);
    };

    $scope.cambioPagina = function () {
        var obj = {
            pagina: $scope.pagina,
            bloque: $scope.bloque,
            nCliente: $scope.ncliente
        };
        LeerClientes(obj);
    };
    var inputPaginaPromise;
    $scope.cambioPaginaTimeout = function () {
        if (inputPaginaPromise) {
            $timeout.cancel(inputPaginaPromise);
        }
        inputPaginaPromise = $timeout($scope.cambioPagina, 1000);
    };
    $scope.paginaAnteriorClientes = function () {
        if ($scope.pagina > 1) {
            $scope.pagina--;
            var obj = {
                pagina: $scope.pagina,
                bloque: $scope.bloque,
                nCliente: $scope.ncliente
            };
            LeerClientes(obj);
        }
    };
    $scope.paginaSiguienteClientes = function () {
        $scope.pagina++;
        var obj = {
            pagina: $scope.pagina,
            bloque: $scope.bloque,
            nCliente: $scope.ncliente
        };
        LeerClientes(obj);
    };

    $scope.files = "";
    $scope.guardarCambios = function (vehiculo) {
        $scope.datagrid.collapseAll(-1);
    };
    $scope.modificarUsuario = function (vid) {
        $scope.popupVisible = true;
        $scope.currentusuario = vid.data;
    };
    $scope.popupVisible = false;
    $scope.popupOptions = {
        width: "90%",
        height: "90%",
        position:"top",
        showTitle: true,
        title: "Editar un usuario",
        dragEnabled: false,
        bindingOptions: {
            visible: 'popupVisible'
        },
        closeOnOutsideClick: false
    };
    $scope.popupInvitarVisible = false;
    $scope.popupInvitarOptions = {
        width: "90%",
        height: "90%",
        position:"top",
        showTitle: true,
        title: "Invitar a un cliente",
        dragEnabled: false,
        bindingOptions: {
            visible: 'popupInvitarVisible'
        },
        closeOnOutsideClick: false
    };
    $scope.abrirPoupupInvitar = function () {
        $scope.popupInvitarVisible = true;
    };
    $scope.invitarCliente = function (cl) {
        var idcl = cl.data.idCliente;
        Llamada.get("ClientesInvitacionesProcesar?IDCliente=" + idcl)
            .then(function (respuesta) {
                mensajeExito("Cliente invitado correctamente.");
                cl.data.idUsuarioWeb = 1;
            });
    };
    $scope.cambioInput = function () {
        alert("Holi");
    };
    $scope.eliminarRegistro = function (a) {
        result = DevExpress.ui.dialog.confirm("¿Seguro que deseas eliminar este usuario?");
        result.then(function (val) {
            if (val) {
                console.log("OK");
                Llamada.get("UsuariosWebEliminar?idUsuarioWeb=" + a.data.idUsuarioWeb)
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
        tipo: "UsuariosWeb",
        cadena: ""
    };
    LeerRegistros(obj);
    $scope.currentcarroceria = {
        descripcion: "Descripción"
    };
    $scope.crearRegistro = function () {
        //alert("Holi");
        $scope.popupVisible = true;
        $scope.currentvehiculo = {
            descripcion: "",
            url: Llamada.getRuta("")
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
    };
    $scope.focofuera = function (a, b) {
        $timeout(fnocultar, 1000);
    };
    $scope.entrafoco = function () {
        if (NotNullNotUndefinedNotEmpty($scope.resultadobusqueda)) {
            $scope.mostrardesplegable = true;
        }
    };
    var inputChangedPromise;
    $scope.cambiobusqueda = function () {
        if (inputChangedPromise) {
            $timeout.cancel(inputChangedPromise);
        }
        inputChangedPromise = $timeout(buscarArticulos, 1000);
    };
    $scope.asignarCliente = function (cl) {
        console.log(cl.data);
        console.log($scope.currentusuario);
        Llamada.get("ClientesAsignarUsuarioWeb?IDUsuarioWeb=" + $scope.currentusuario.idUsuarioWeb + "&IDCliente=" + cl.data.idCliente)
            .then(function (respuesta) {
                console.log(respuesta.data);
                $scope.popupVisible = false;
                
                mensajeExito("Usuario asignado a un cliente correctamente.");
                LeerRegistros($scope.lastConsulta);
            });
    };
    
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
    };
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
    };
    $scope.popupPermisosOptions = {
        width: 660,
        height: "auto",
        showTitle: true,
        title: "Permisos de usuario",
        dragEnabled: false,
        bindingOptions: {
            visible: 'popupPermisosVisible'
        },
        closeOnOutsideClick: true
    }
    $scope.modificarPermisos = function (usuario) {
        $scope.popupPermisosVisible = true;
        Llamada.get("PermisosLeerPorIDUsuario?idUsuarioWeb=" + usuario.idUsuarioWeb)
            .then(function (respuesta) {
                $scope.usuarioseleccionado = usuario;
                $scope.permisosUs = respuesta.data;
            });
    }
    $scope.cancelarCambiosPermisos = function () {
        $scope.popupPermisosVisible = false;
    }
    $scope.guardarCambiosPermisos = function () {
        Llamada.post("PermisosUsuarioModificar?idUsuarioWeb=" + $scope.usuarioseleccionado.idUsuarioWeb, $scope.permisosUs)
            .then(function (respuesta) {
                console.log(respuesta.data);
                $scope.popupPermisosVisible = false;
                mensajeExito("Datos guardados con éxito");
            })
    }


    var buscaChangePromise;
    $scope.cambioBuscador = function () {
        if (buscaChangePromise) {
            $timeout.cancel(buscaChangePromise);
        }
        buscaChangePromise = $timeout($scope.activarBusqueda, 1000);
    };
    $scope.activarBusqueda = function () {
        console.log("Ok, busco con");
        console.log($scope.buscador);
        var obj = {
            tipo: "UsuariosWeb",
            cadena: $scope.buscador
        };
        LeerRegistros(obj);
    };
    $scope.anularBusqueda = function () {
        console.log("Ok, anulo búsqueda");
        $scope.buscador = "";
        var obj = {
            tipo: "UsuariosWeb",
            cadena: ""

        };
        LeerRegistros(obj);
    };
});