$(document).ready(function () {
    
    // Function to generate a random nonce
    function generateNonce() {
        //return Math.random().toString(36).substring(2, 10); // Adjust the length as needed
        var test = "ihYxAijSER-YFSUxCDJbag";
        return test;
    }

    // Function to add nonce to a script element
    function addNonceToScript(scriptElement) {
        var nonce = generateNonce(); // Generate a new nonce
        scriptElement.setAttribute('nonce', nonce);
    }      

    // Function to add nonce to all existing and dynamically added scripts
    function addNonceToAllScripts() {
        var scripts = document.querySelectorAll('script');         
        scripts.forEach(function (script) {
            addNonceToScript(script);
        });
    }      

    // Add nonce to all scripts after the page is fully loaded
    window.addEventListener('load', addNonceToAllScripts);     

});