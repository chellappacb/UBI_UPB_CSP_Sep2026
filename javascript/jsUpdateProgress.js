var postbackElement = null;

function beginReq(sender, args) {
    try {
        postbackElement = args.get_postBackElement();
        if ($find("mpe_Confirm") != null) {
            $find("mpe_Confirm").hide();
        }
        $find(ModalProgress).show();
        //$('#ModalProgress').show();
        //console.log("5");
    }
    catch (err) {
        if ($find("mpe_Confirm") != null) {
            $find("mpe_Confirm").hide();
        }
        $find(ModalProgress).show();
        //$('#ModalProgress').show();
        //console.log("6");
    }
}

function endReq(sender, args) {
    try {
        $find(ModalProgress).hide();
        //$('#ModalProgress').hide();
        document.getElementById(postbackElement.id).focus();
        //console.log("7");
    }
    catch (err) {
        $find(ModalProgress).hide();
        //$('#ModalProgress').hide();
        console.log("8");
    }
}

function AddRequestHandler() {
    console.log("start1");
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(endReq);
    prm.add_beginRequest(beginReq);
}