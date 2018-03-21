appadmin.controller('Articulos', function ($scope, Llamada, $timeout) {
    //console.log("Holi");
    $scope.anadiendoCategoria = false;
    $scope.anadiendoAccesorio = false;
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
            //console.log(info);
            //console.log(a);
        },
        onInitNewRow: function (info, b) {
            //console.log("init");
            //console.log(info);
            info.data.descripcion = 'Nuevo';
            //console.log("HOLIII");
            info.component.saveEditData();
        },
        /*onSelectionChanged: function (e) {
            e.component.collapseAll(-1);
            e.component.expandRow(e.currentSelectedRowKeys[0]);
        },*/
        onRowRemoving: function (e) {
            //console.log(e);
            eliminarRegistro(e.data.idCarroceria);
        },
        columns: [
            {
                dataField: "descripcion",
                width: "80%",
                caption: "Descripcion"
            }, {
                caption: "",
                width: "20%",
                alignment: "center",
                allowFiltering: false,
                allowSorting: false,
                allowEditing: false,
                cellTemplate: "editTemplate"
            }/*, {
                caption: "",
                width: 80,
                allowFiltering: false,
                allowSorting: false,
                allowEditing: false,
                cellTemplate: "deleteTemplate"
            }*/
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
            //console.log(e);
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
                    //console.log($scope.orders);
                    //console.log("Arriba las orders");
                    $scope.datagrid.option("dataSource", $scope.orders);
                }
                //$scope.datagrid.repaint();
            });
    };

    $scope.files = "";
    $scope.guardarCambios = function (articulo) {
        console.log("OK");
        console.log($scope.lastCategoria);
        $scope.datagrid.collapseAll(-1);
        if (NotNullNotUndefinedNotEmpty($scope.lastCategoria)) {
            articulo.idCategoria = $scope.lastCategoria.idCategoria;
            articulo.descripcionCategoria = $scope.lastCategoria.descripcion;
        }
        if (NotNullNotUndefinedNotEmpty($scope.lastVidrio)) {
            articulo.descripcionTipoVidrio = $scope.lastVidrio.descripcion;

            articulo.idTipoVidrio = $scope.lastVidrio.idTipoVidrio;
        }
        try {
            

            articulo.descripcionSeccion = $scope.lastMarca.descripcionSeccion;
            articulo.descripcionFamilia = $scope.lastModelo.descripcionFamilia;
            


            articulo.idSeccion = $scope.lastMarca.idSeccion;
            articulo.idFamilia = $scope.lastModelo.idFamilia;
        } catch(ex) {

        }
        guardarCambios(articulo);
    };
    $scope.modificarArticulo = function (vid) {
        try {
            $scope.selectElement.option("value", undefined);
            $scope.selectElement3.option("value", undefined);
            $scope.selectElement2.option("value", undefined);

        } catch (ex) {
            //console.log(ex);
            //alert("Error");
        }
        //console.log(vid);
        $scope.currentarticulo = vid.data;
        Llamada.get("ArticulosLeerPorID?IDArticulo=" + vid.data.idArticulo)
            .then(function (respuesta) {
                //console.log(respuesta);
                $scope.popupVisible = true;
                $scope.anadiendoAccesorio = false;
                $scope.anadiendoCategoria = false;
                $scope.selectedCat = null;
                $scope.currentarticulo = respuesta.data;
                $scope.lastMarca = {
                    idSeccion: $scope.currentarticulo.idSeccion,
                    descripcionSeccion: $scope.currentarticulo.descripcionSeccion
                };
                $scope.lastModelo = {
                    idFamilia: $scope.currentarticulo.idFamilia,
                    descripcionFamilia: $scope.currentarticulo.descripcionFamilia
                };
                $scope.lastVidrio = {
                    idTipoVidrio: $scope.currentarticulo.idTipoVidrio,
                    descripcion: $scope.currentarticulo.descripcionTipoVidrio
                };
                $scope.lastCategoria = {
                    idCategoria: $scope.currentarticulo.idCategoria,
                    descripcion: $scope.currentarticulo.descripcionCategoria
                };
                console.log($scope.lastCategoria);
                //alert("Inicializada");
                Llamada.get("CategoriasLeer")
                    .then(function (respuesta) {
                        $scope.categorias = respuesta.data;
                        //$scope.selectboxcategs.option("dataSource", $scope.categorias);
                        //$scope.tabpanel.option("dataSource", $scope.categorias);
                        $scope.datagridcats.option("dataSource", $scope.currentarticulo.accesorios);
                        //alert("Holiii");
                        $scope.selectElement4.option("value", $scope.lastCategoria.idCategoria);
                        $scope.selectElement3.option("value", $scope.lastVidrio.idTipoVidrio);
                        $scope.datagridcarrocerias.option("dataSource", $scope.currentarticulo.carrocerias);
                    })
            })
        
        /*if (NotNullNotUndefinedNotEmpty($scope.currentarticulo.idSeccion)) {
            buscaModelos($scope.lastMarca);
        }*/
        //console.log(Currentarticulo);
    };
    guardarCambios = function (art) {
        art.files = null;
        //console.log(art);
        //console.log("Hasta aqui he llegado!");
        //console.log(art);
        art.accesoriosinsertar = [];
        art.accesorioseliminar = [];
        for (i = 0; i < art.accesorios.length; i++) {
            if (!NotNullNotUndefinedNotEmpty(art.accesorios[i].articulos)) {
                art.accesorios[i].articulos = [];
            }
            if (art.accesorios[i].articulos.length > 0) {
                for (s = 0; s < art.accesorios[i].articulos.length; s++) {
                    art.accesorios[i].articulos[s].idCategoria = art.accesorios[i].idCategoria;
                    art.accesorios[i].articulos[s].idArticuloRel = art.accesorios[i].articulos[s].idArticulo;
                    art.accesoriosinsertar.push(art.accesorios[i].articulos[s]);
                }
                
            } else {
                var c = {
                    idArticuloCategoria: art.accesorios[i].idArticuloCategoria,
                    idCategoria: art.accesorios[i].idCategoria,
                    idArticuloRel: null
                }
                art.accesoriosinsertar.push(c);
            }
        }
        if (NotNullNotUndefinedNotEmpty(art.accesoriosElim)) {
            for (q = 0; q < art.accesoriosElim.length; q++) {
                art.accesorioseliminar.push(art.accesoriosElim[q]);
            }
        }
        if (NotNullNotUndefinedNotEmpty(art.articulosElim)) {

            for (r = 0; r < art.articulosElim.length; r++) {
                art.articulosElim[r].idArticuloRel = art.articulosElim[r].idArticulo;
                art.accesorioseliminar.push(art.articulosElim[r]);
            }
        }
        for (x = 0; x < art.accesoriosinsertar.length; x++) {
            art.accesoriosinsertar[x].tipoOperacion = 1;
        }
        art.accesoriosinsertar = art.accesoriosinsertar.concat(art.accesorioseliminar);
        art.accesorios = null;
        //console.log(art);
        art.carrocerias = $scope.datagridcarrocerias.option("dataSource");
        console.log($scope.datagridcarrocerias)
        var c = 5;
        Llamada.post("ArticulosModificar", art)
            .then(function (respuesta) {
                mensajeExito("Datos guardados con éxito");
                $scope.popupVisible = false;
            });
        $scope.popupVisible = false;
        

    };
    $scope.popupVisible = false;
    $scope.popupOptions = {
        width: "95%",
        height: '95%',
        showTitle: true,
        title: "Modificar Artículo",
        fullScreen:false,
        dragEnabled: false,
        bindingOptions: {
            visible: 'popupVisible'
        },
        closeOnOutsideClick: true
    };
    $scope.cambioInput = function () {
        alert("Holi");
    };
    validarFechas = function () {
        var res = true;
        var carroceriasvalidadas = JSON.parse("" + JSON.stringify($scope.datagridcarrocerias.option("dataSource")));
        if (NotNullNotUndefinedNotEmpty(carroceriasvalidadas)) {
            console.log("No están nulas");
            for (i = 0; i < carroceriasvalidadas.length; i++) {
                console.log("Recorriendo");
                var ano = carroceriasvalidadas[i].anos;
                if (NotNullNotUndefinedNotEmpty(ano)) {
                    console.log("Ano no nulo");
                    var anosplit = ano.split("-");
                    if (isNaN(anosplit[0])) {
                        res = false;
                    }
                    if (isNaN(anosplit[1])) {
                        res = false;
                    }
                } else {
                    console.log("Ano es nulo");
                }
            }
        }
        return res;
    }
    $scope.selectBox = {
        dataSource: [],
        displayExpr: "descripcionSeccion",
        valueExpr: "idSeccion",
        searchEnabled: true,
        height:"30px",
        noDataText: 'No se ha encontrado ninguna marca',
        //value: products[0].idSeccion,
        fieldTemplate: 'field',
        onInitialized: function (e) {
            $scope.selectElement = e.component;
            oldValue = e.component.option("value");
        },
        onValueChanged: function (e) {
            //console.log(e);
            if (NotNullNotUndefinedNotEmpty(e.component._options.selectedItem)) {
                //console.log(e);
                //console.log(e.component._options);
                //console.log(e.component._options.selectedItem);
                $scope.lastMarca = JSON.parse("" + JSON.stringify(e.component._options.selectedItem));
                buscaModelos($scope.lastMarca);
            } else {
                //console.log(e);
                //console.log("Está vacío!!");
                oldValue = e.previousValue;
            }

        }
    };
    $scope.selectBox2 = {
        dataSource: [],
        valueExpr: "idFamilia",
        placeholder: "Selecciona un modelo",
        noDataText: 'Selecciona la marca primero',
        displayExpr: "descripcionFamilia",
        //value: products[0].idFamilia,
        onInitialized: function (e) {
            $scope.selectElement2 = e.component;
        },
        onValueChanged: function (e) {
            if (NotNullNotUndefinedNotEmpty(e.component._options.selectedItem)) {
                //console.log(e);
                //console.log(e.component._options);
                //console.log(e.component._options.selectedItem);
                $scope.lastModelo = JSON.parse("" + JSON.stringify(e.component._options.selectedItem));
                //console.log("Mira el modelo:");
                //console.log($scope.lastModelo);
            }

        }
    };

    $scope.selectBox3 = {
        dataSource: [],
        displayExpr: "descripcion",
        placeholder: "Selecciona un tipo de vidrio",
        noDataText: 'No se han encontrado vidrios',
        valueExpr: "idTipoVidrio",
        //searchEnabled: true,
        //value: products[0].idTipoVehiculo,
        //fieldTemplate: 'field',
        onInitialized: function (e) {
            $scope.selectElement3 = e.component;
            Llamada.get("TiposVidrioLeer")
                .then(function (respuesta) {
                    //console.log(respuesta.data);
                    $scope.tiposvid = respuesta.data;
                    $scope.tiposvid.splice(0, 0, { descripcionTipoVidrio: "Sin Tipo", descripcion: "Sin Tipo", idTipoVidrio: null });
                    for (i = 0; i < $scope.tiposvid.length; i++) {
                        $scope.tiposvid[i].url = Llamada.getRuta($scope.tiposvid[i].imagen);
                    }
                    if (NotNullNotUndefinedNotEmpty($scope.selectElement3)) {
                        $scope.selectElement3.option("dataSource", $scope.tiposvid);
                    }

                });
        },
        onValueChanged: function (e) {
            //console.log(e);
            $scope.lastVidrio = e.component._options.selectedItem;
            console.log(e.component._options.selectedItem);
        }
    };

    $scope.selectBox4 = {
        dataSource: [],
        displayExpr: "descripcion",
        placeholder: "Selecciona una categoría",
        noDataText: 'No se han encontrado categorías',
        valueExpr: "idCategoria",
        //searchEnabled: true,
        //value: products[0].idTipoVehiculo,
        //fieldTemplate: 'field',
        onInitialized: function (e) {
            $scope.selectElement4 = e.component;
            Llamada.get("CategoriasLeer")
                .then(function (respuesta) {
                    //console.log(respuesta.data);
                    $scope.categorias = respuesta.data;
                    $scope.categorias.splice(0, 0, { descripcion: "Sin Categoría", idCategoria: null })

                    if (NotNullNotUndefinedNotEmpty($scope.selectElement4)) {
                        $scope.selectElement4.option("dataSource", $scope.categorias);
                    }

                });
        },
        onValueChanged: function (e) {
            //console.log(e);
            $scope.lastCategoria = e.component._options.selectedItem;
            console.log(e.component._options.selectedItem);
        }
    };
    

    buscaMarcas = function (val) {
        //console.log("Ahora  busca marcas");
        //console.log(val);
        //console.log($scope.newValor);
        Llamada.get("MarcasLeerPorCadena?cadena=" + $scope.newValor)
            .then(function (respuesta) {
                //console.log(respuesta.data);
                //console.log($scope.selectElement);
                $scope.marcas = respuesta.data;
                for (i = 0; i < $scope.marcas.length; i++) {
                    $scope.marcas[i].url = Llamada.getRuta($scope.marcas[i].imagen);
                }
                $scope.selectElement.option("dataSource", $scope.marcas);
                $scope.selectElement.open();
            });
    };
    buscaModelos = function (val) {
        //console.log("Ahora  busca modelos");
        //console.log(val);
        //console.log($scope.newValor);
        Llamada.get("ModelosLeerPorMarca?IDSeccion=" + val.idSeccion)
            .then(function (respuesta) {
                //console.log(respuesta.data);
                //console.log($scope.selectElement);
                $scope.modelos = respuesta.data;
                for (i = 0; i < $scope.modelos.length; i++) {
                    $scope.modelos[i].url = Llamada.getRuta($scope.modelos[i].imagen);
                }
                $scope.selectElement2.option("dataSource", $scope.modelos);
                $scope.selectElement2.open();
            });
    };

    var inputChangedPromise;
    $scope.cambiobusqueda = function () {
        if (inputChangedPromise) {
            $timeout.cancel(inputChangedPromise);
        }
        inputChangedPromise = $timeout(buscaMarcas, 1000);
    };
    $scope.settingsbox = {
        placeholder: 'Selecciona una Marca',
        bindingOptions: {
            value: 'textBoxValue'
        },
        onInitialized: function (e) {
            $scope.campotexto = e.component;
        },
        valueChangeEvent: "keyup",
        onKeyUp(e) {
            $scope.newValor = e.component.option("value");
            $scope.cambiobusqueda();
        }
    };
    $scope.inittext = function (e) {
        //console.log(e);
        //console.log("Aqui se inicializa");
        //console.log(e);
        $scope.campotexto = e.component;
    };
    $scope.eliminarRegistro = function (id) {
        result = DevExpress.ui.dialog.confirm("¿Seguro que deseas eliminar esta carrocería?");
        result.then(function (val) {
            if (val) {
                

                Llamada.get("CarroceriasEliminar?idCarroceria=" + id.data.idCarroceria)
                    .then(function (respuesta) {
                        //console.log(respuesta);
                        
                            LeerRegistros($scope.lastConsulta);
                    });
            }
        });

        
    };
    $scope.hola = function () {
        alert("Hola");
    };
    var obj = {
        tipo: "Articulo",
        cadena: ""
    };
    LeerRegistros(obj);
    $scope.currentarticulo = {
        descripcion: "Descripción"
    };
    $scope.guardarCambiosPopup = function () {
        //alert("Guardando");
        if (validarFechas()) {
            $scope.guardarCambios($scope.currentarticulo);
        } else {
            mensajeError("Las fechas introducidas deben tener la sintaxis XXXX-XXXX");
        }
        
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
    buscarArticulos = function () {
        $scope.loading = true;
        if ($scope.cadena.length > 0) {
            Llamada.get("TiposVidrioLeerPorCadena?cadena=" + $scope.cadena)
                .then(function (respuesta) {
                    $scope.resultadobusqueda = respuesta.data;
                    //$scope.NumReg = respuesta.data.numReg;
                    $scope.mostrardesplegable = true;
                    $scope.loading = false;
                    document.getElementById("desplegable").style.display = "block";
                });
        } else {
            $scope.resultadobusqueda = [];
            $scope.NumReg = 0;
            $scope.loading = false;
        }
    };
    
    /*$scope.anadirVidrio = function (res) {
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
    }*/
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
                //console.log("OK");
                $scope.$apply(function () {
                    vidrio.modificador = 2;
                });


                //console.log(vidrio);
            }
        });
    };
    $scope.cancelarCambios = function () {
        $scope.popupVisible = false;
    };



    var buscaChangePromise;
    $scope.cambioBuscador = function () {
        if (buscaChangePromise) {
            $timeout.cancel(buscaChangePromise);
        }
        buscaChangePromise = $timeout($scope.activarBusqueda, 1000);
    };
    $scope.activarBusqueda = function () {
        //console.log("Ok, busco con");
        //console.log($scope.buscador);
        var obj = {
            tipo: "Articulo",
            cadena: $scope.buscador
        };
        LeerRegistros(obj);
    };
    $scope.anularBusqueda = function () {
        //console.log("Ok, anulo búsqueda");
        $scope.buscador = "";
        var obj = {
            tipo: "Articulo",
            cadena: ""

        };
        LeerRegistros(obj);
    };
    $scope.selectBoxCategorias = {
        dataSource: [],
        displayExpr: "descripcion",
        valueExpr: "idCategoria",
        searchEnabled: true,
        height: "30px",
        noDataText: 'No se ha encontrado ninguna categoría',
        //value: products[0].idSeccion,
        onInitialized: function (e) {
            $scope.selectElementCategorias = e.component;
        },
        onValueChanged: function (e) {
            //console.log(e);
            /*if (NotNullNotUndefinedNotEmpty(e.component._options.selectedItem)) {
                console.log(e);
                console.log(e.component._options);
                console.log(e.component._options.selectedItem);
                $scope.lastMarca = JSON.parse("" + JSON.stringify(e.component._options.selectedItem));
                buscaModelos($scope.lastMarca);
            } else {
                console.log(e);
                console.log("Está vacío!!");
                oldValue = e.previousValue;
            }*/

        }
    };
    $scope.selectBoxAccesorios = {
        dataSource: [],
        displayExpr: "descripcion",
        valueExpr: "idArticulo",
        searchEnabled: true,
        height: "30px",
        noDataText: 'No se ha encontrado ningun artículo',
        //value: products[0].idSeccion,
        onInitialized: function (e) {
            $scope.selectElementAccesorios = e.component;
            oldValue = e.component.option("value");
        },
        onValueChanged: function (e) {
            //console.log(e);
            /*if (NotNullNotUndefinedNotEmpty(e.component._options.selectedItem)) {
                console.log(e);
                console.log(e.component._options);
                console.log(e.component._options.selectedItem);
                $scope.lastMarca = JSON.parse("" + JSON.stringify(e.component._options.selectedItem));
                buscaModelos($scope.lastMarca);
            } else {
                console.log(e);
                console.log("Está vacío!!");
                oldValue = e.previousValue;
            }*/

        }
    }
    $scope.settingsBoxAccesorios = {

    }
    $scope.dataGridAccesorios = {
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
        onRowInserted: function (info, a) {
            //console.log(info);
            //console.log(a);
        },
        onRowRemoving: function (e) {
            //console.log(e);
            alert("Esto no está hecho bien");

            //eliminarRegistro(e.data.idCarroceria);
        },
        columns: [
            {
                dataField: "descripcion",
                width: "80%",
                caption: "Descripcion"
            }, {
                caption: "",
                width: "20%",
                alignment: "center",
                allowFiltering: false,
                allowSorting: false,
                allowEditing: false,
                cellTemplate: "deleteTemplate"
            }
        ],
        onInitialized: function (e) {
            //console.log(e);
            $scope.datagridaccs = e.component;
        }
    }

    $scope.dataGridCarrocerias = {
        dataSource: [],
        keyExpr: "idModeloCarroceria",
        editing: {
            allowAdding: false, // Enables insertion
            allowDeleting: false, // Enables removing
            editEnabled: true
        },
        selection: {
            mode: "single"
        },
        columns: [
            {
                dataField: "descripcionSeccion",
                width: "80%",
                caption: "Marca",
                allowEditing: false,
            }, {
                dataField: "descripcionFamilia",
                width: "80%",
                caption: "Modelo",
                allowEditing: false,
            }, {
                dataField: "descripcionCarroceria",
                width: "80%",
                caption: "Modelo",
                allowEditing: false,
            }, {
                dataField: "descripcionArticuloModelo",
                width: "80%",
                caption: "Descripcion",
                //cellTemplate: "descriptionTemplate",
            }, {
                dataField: "anos",
                width: "80%",
                caption: "Años",
                //cellTemplate: "anosTemplate",
            }, 
        ],
        onInitialized: function (e) {
            //console.log(e);
            $scope.datagridcarrocerias = e.component;
        }
    }


    /*$scope.tabPanelOptions = {
        height: 260,
        dataSource: [],
        itemTemplate: "customer",
        bindingOptions: {
            selectedIndex: "selectedIndex",
            loop: "loop",
            animationEnabled: "animationEnabled",
            swipeEnabled: "swipeEnabled"
        },
        onInitialized: function (e) {
            console.log(e);
            $scope.tabpanel = e.component;
        },
        onSelectionChanged: function (e) {
            console.log(e);
            console.log(e.component._options.selectedItem);
            idcategoria = e.component._options.selectedItem.idCategoria;
            Llamada.get("ArticulosLeerPorCategoria?IDCategoria=" + idcategoria)
                .then(function (respuesta) {
                    $scope.selectboxcatc.option("dataSource", respuesta.data);
                    $scope.datagridaccs.option("dataSource", e.articulos)
                })
        }
    };*/
    $scope.selectoboxcat = {
        dataSource: [],
        displayExpr: 'descripcion',
        valueExpr: 'idArticulo',
        searchEnabled: true,
        fieldTemplate: 'field',
        onInitialized: function (e) {
            //console.log(e);
            $scope.selectboxcatc = e.component;

            Llamada.get("ArticulosLeerPorCategoria?IDCategoria=" + $scope.selectedCat.idCategoria)
                .then(function (respuesta) {
                    //console.log("holii");
                    $scope.selectboxcatc.option("dataSource", respuesta.data);
                    // $scope.datagridaccs.option("dataSource", e.articulos)
                })


        },
        onValueChanged: function (e) {
            //console.log(e);
            try {
                console.log("aqui");
                console.log($scope.selectedCat.articulos[0].descripcion);
            }
            catch (ex) {

            }
            $scope.lastAccesorio = JSON.parse("" + JSON.stringify(e.component._options.selectedItem));
            if (NotNullNotUndefinedNotEmpty($scope.lastAccesorio)) {
                try {
                    console.log("aqui2");
                    console.log($scope.selectedCat.articulos[0].descripcion);
                }
                catch (ex) {

                }
                $scope.anadiendoAccesorio = false;
                //console.log($scope.lastAccesorio);
                idcategoria = $scope.selectedCat.idCategoria;
                for (i = 0; i < $scope.currentarticulo.accesorios.length; i++) {

                    if ($scope.currentarticulo.accesorios[i].idCategoria == idcategoria) {
                        if (!NotNullNotUndefinedNotEmpty($scope.currentarticulo.accesorios[i].articulos)) {
                            $scope.currentarticulo.accesorios[i].articulos = [];
                        }
                        var found = false;

                        for (r = 0; r < $scope.currentarticulo.accesorios[i].articulos.length; r++) {
                            if ($scope.currentarticulo.accesorios[i].articulos[r].idArticulo == $scope.lastAccesorio.idArticulo) {
                                found = true;
                            }
                        }
                        try {
                            console.log("aqui5");
                            console.log($scope.selectedCat.articulos[0].descripcion);
                        }
                        catch (ex) {

                        }
                        
                        try {
                            console.log("aqui6");
                            console.log($scope.selectedCat.articulos[0].descripcion);
                        }
                        catch (ex) {

                        }
                        if (!found) {
                            var articulo = JSON.parse("" + JSON.stringify($scope.lastAccesorio))
                            //$scope.currentarticulo.accesorios[i].articulos.push(articulo);
                            var eliminado = false;
                            for (w = 0; w < $scope.currentarticulo.accesorios[i].articulos.length; w++) {
                                //console.log("hola" + $scope.currentarticulo.accesorios[i].articulos[w].idCategoria);
                                if (ZeroSiNull($scope.currentarticulo.accesorios[i].articulos[w].idArticulo) == 0) {
                                    if (!eliminado) {
                                        $scope.currentarticulo.accesorios[i].articulos[w].idCategoria = articulo.idCategoria;
                                        $scope.currentarticulo.accesorios[i].articulos[w].descripcion = articulo.descripcion;
                                        $scope.currentarticulo.accesorios[i].articulos[w].idArticulo = articulo.idArticulo;
                                        eliminado = true;
                                    }
                                    

                                }
                            }


                        } else {
                            mensajeError("Accesorio duplicado");
                        }
                        var eliminado = false;
                        
                       
                        //alert("Añadido");
                        //console.log($scope.currentarticulo);
                        $scope.datagridarticuloscat.option("dataSource", $scope.currentarticulo.accesorios[i].articulos)
                    }
                }
            }
            
        }
    }

    buscaAccesorios = function () {
        idcategoria = $scope.selectedCat.idCategoria;
        //console.log($scope.newValorAccesorio);
        Llamada.get("ArticulosLeerPorCategoriaYCadena?IDCategoria=" + idcategoria + "&Cadena=" + $scope.newValorAccesorio)
            .then(function (respuesta) {
                $scope.selectboxcatc.option("dataSource", respuesta.data);
            })
    }
    $scope.eliminarArticulo = function (art) {
        result = DevExpress.ui.dialog.confirm("¿Seguro que deseas eliminar este accesorio?");
        result.then(function (val) {
            if (val) {
                idcategoria = $scope.tabpanel._options.selectedItem.idCategoria;
                //console.log(art);
                
            }
        });
    }
    var inputChangedAccesoriosPromise;
    $scope.cambiobusquedaaccesorios = function () {
        if (inputChangedAccesoriosPromise) {
            $timeout.cancel(inputChangedAccesoriosPromise);
        }
        inputChangedAccesoriosPromise = $timeout(buscaAccesorios, 1000);
    };

    $scope.settingsboxAccesorios = {
        placeholder: 'Busca un accesorio',
        onInitialized: function (e) {
            $scope.campotextoaccesorio = e.component;
        },
        valueChangeEvent: "keyup",
        onKeyUp(e) {
            $scope.newValorAccesorio = e.component.option("value");
            $scope.cambiobusquedaaccesorios();
        }
    };
    $scope.dataGridCategorias = {
        dataSource: [],
        keyExpr: "id",
        editing: {
            allowAdding: false, // Enables insertion
            allowDeleting: false, // Enables removing
            editEnabled: false
        },
        noDataText:"Añadir Categoría de Accesorios",
        selection: {
            mode: "single"
        },
        onRowInserted: function (info, a) {
            //console.log(info);
            //console.log(a);
        },
        onRowRemoving: function (e) {
            //console.log(e);
            alert("Esto no está hecho bien");
            //eliminarRegistro(e.data.idCarroceria);
        },
        columns: [
            {
                dataField: "descripcion",
                width: "50%",
                caption: "Categorías",
                cellTemplate:"descriptionTemplate"
                
                
            }, {
                caption: "Eliminar",
                width: "25%",
                alignment: "center",
                allowFiltering: false,
                allowSorting: false,
                allowEditing: false,
                cellTemplate: "deleteTemplate"
            }, {
                caption: "Ver Artículos",
                width: "25%",
                alignment: "center",
                allowFiltering: false,
                allowSorting: false,
                allowEditing: false,
                cellTemplate: "editTemplate"
            }
        ],
        onInitialized: function (e) {
            //console.log(e);
            $scope.datagridcats = e.component;
        }
    }
    $scope.modificarCat = function (cat) {
        //console.log(cat);
        if (cat.modificando === true) {
            cat.modificando = false;
        } else {
            cat.modificando = true;
        }
        
    }
    $scope.dataGridArticulosCat = {
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
        onRowInserted: function (info, a) {
            //console.log(info);
            //console.log(a);
        },
        onRowRemoving: function (e) {
            //console.log(e);
            alert("Esto no está hecho bien");
            //eliminarRegistro(e.data.idCarroceria);
        },
        columns: [
            {
                dataField: "descripcion",
                width: "50%",
                caption: "Descripción",
                cellTemplate: "descriptionTemplate"


            },  {
                caption: "",
                width: "20%",
                alignment: "center",
                allowFiltering: false,
                allowSorting: false,
                allowEditing: false,
                cellTemplate: "deleteTemplate"
            }
        ],
        onInitialized: function (e) {
            //console.log(e);
            $scope.datagridarticuloscat = e.component;
        }
    }
    $scope.mostrarArticulosCat = function (Cat) {
        //console.log(Cat);
        $scope.selectedCat = $scope.currentarticulo.accesorios[Cat.rowIndex];
        //console.log($scope.selectedCat);
        $scope.datagridarticuloscat.option("dataSource", $scope.selectedCat.articulos);
        //alert("Holiiii");
        //console.log(Cat);
        $scope.datagridarticuloscat.columnOption(0, "caption", Cat.data.descripcion)
        
    }
    $scope.eliminarCategoria = function (a) {
        result = DevExpress.ui.dialog.confirm("¿Seguro que deseas eliminar esta categoría?");
        result.then(function (val) {
            if (val) {
                if (!NotNullNotUndefinedNotEmpty($scope.currentarticulo.accesoriosElim)) {
                    $scope.currentarticulo.accesoriosElim = []
                }

                var b = $scope.currentarticulo.accesorios.splice(a.rowIndex, 1);
                $scope.currentarticulo.accesoriosElim.push(b[0]);
                $scope.datagridcats.option("dataSource", $scope.currentarticulo.accesorios);
                $scope.selectedCat = null;
            }
        });
    }
    $scope.SelectBoxCategs = {
        dataSource: [],
        valueExpr: "idCategoria",
        placeholder: "Selecciona una Categoría",
        displayExpr: "descripcion",
        //value: products[0].idFamilia,
        onInitialized: function (e) {
            $scope.selectboxcategs = e.component;
            Llamada.get("CategoriasLeer")
                .then(function (respuesta) {
                    $scope.categorias = respuesta.data;
                    $scope.selectboxcategs.option("dataSource", $scope.categorias);
                    console.log("Holi")
                    console.log($scope.selectboxcategs)
                })
        },
        onValueChanged: function (e) {
            if (NotNullNotUndefinedNotEmpty(e.component._options.selectedItem)) {
                console.log(e);
                console.log(e.component._options.selectedItem);
                if (!NotNullNotUndefinedNotEmpty($scope.currentarticulo.accesorios)) {
                    $scope.currentarticulo.accesorios = [];
                }
                var found = false;
                for (h = 0; h < $scope.currentarticulo.accesorios.length; h++) {
                    if ($scope.currentarticulo.accesorios[h].idCategoria == e.component._options.selectedItem.idCategoria) {
                        found = true;
                    }
                }
                if (!found) {
                    $scope.currentarticulo.accesorios.push(e.component._options.selectedItem);
                   
                } else {
                    mensajeError("Categoria Repetida");
                }
                for (i = 0; i < $scope.currentarticulo.accesorios.length; i++) {
                    if ($scope.currentarticulo.accesorios[i].idCategoria == 0) {
                        $scope.currentarticulo.accesorios.splice(i, 1);
                    }
                }
                $scope.datagridcats.option("dataSource", $scope.currentarticulo.accesorios);
                $scope.anadiendoCategoria = false;
                /*console.log(e);
                console.log(e.component._options);
                console.log(e.component._options.selectedItem);
                $scope.lastModelo = JSON.parse("" + JSON.stringify(e.component._options.selectedItem));
                console.log("Mira el modelo:");
                console.log($scope.lastModelo);*/
            }

        }
    };
    $scope.eliminarArticulos = function (art) {
        //console.log(art);
        result = DevExpress.ui.dialog.confirm("¿Seguro que deseas eliminar este artículo?");
        result.then(function (val) {
            if (val) {
                var c = $scope.selectedCat.articulos.splice(art.rowIndex, 1);
                if (!NotNullNotUndefinedNotEmpty($scope.currentarticulo.articulosElim)) {
                    $scope.currentarticulo.articulosElim = [];
                }
                console.log(c);
                $scope.currentarticulo.articulosElim.push(c[0]);
                $scope.datagridarticuloscat.option("dataSource", $scope.selectedCat.articulos);
            }
        });
        
    }
    $scope.anadirColumna = function () {
        //console.log($scope.datagridcats)
        if (!$scope.anadiendoCategoria) {
            $scope.currentarticulo.accesorios.push({
                idCategoria: 0,
                descripcion: null,
            })
            $scope.datagridcats.option("dataSource", $scope.currentarticulo.accesorios);
            $scope.anadiendoCategoria = true;
        }
        

    }
    $scope.anadirColumnaArt = function () {

        if (!$scope.anadiendoAccesorio) {
            if (!NotNullNotUndefinedNotEmpty($scope.selectedCat.articulos)) {
                $scope.selectedCat.articulos = [];
            }
            //console.log($scope.selectedCat);

            $scope.selectedCat.articulos.push({ idCategoria: 0, descripcion: null })
            $scope.datagridarticuloscat.option("dataSource", $scope.selectedCat.articulos);
            $scope.anadiendoAccesorio = true;
        } 
    }
});