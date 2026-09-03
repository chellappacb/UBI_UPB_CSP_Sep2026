var postbackElement = null;

function beginReq(sender, args) {
    try {
        postbackElement = args.get_postBackElement();
        //$find(ModalProgress).show();
        //$('#ModalProgress').show();
        $('#ModalProgress').removeClass();
        $('#ModalProgress').addClass("modal  d-block ");
        //console.log("1");
    }
    catch (err) {
        //$find(ModalProgress).show();
        //$('#ModalProgress').show();
        $('#ModalProgress').removeClass();
        $('#ModalProgress').addClass("modal  d-block ");
        //console.log("2");
    }
}

function endReq(sender, args) {
    try {
        //$find(ModalProgress).hide();
        //$('#ModalProgress').hide();
        $('#ModalProgress').removeClass();
        $('#ModalProgress').addClass("modal  d-none ");

        document.getElementById(postbackElement.id).focus();
        //console.log("3");
    }
    catch (err) {
        //$find(ModalProgress).hide();
        //$('#ModalProgress').hide(); 
        $('#ModalProgress').removeClass();
        $('#ModalProgress').addClass("modal  d-none ");
        //console.log("4");
    }
}

function AddRequestHandler() {
    //console.log("start2");
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(endReq);
    prm.add_beginRequest(beginReq);
}