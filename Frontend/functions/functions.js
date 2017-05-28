var cy = cytoscape({
  container: document.getElementById('cy'),
  elements: [
    { data: { id: 'a', name: 'decition', text: 'Hello', url: 'asd.com', root: 'yes' } },
    ],
    style: [
      {
				selector: 'node',
				css: {
          'background-color': 'grey',
          'width':'100px',
          'height':'100px',
          label: 'data(name)',
				}
			},
			{
				selector: 'edge',
				css: {
					'curve-style': 'unbundled-bezier',
					'target-arrow-shape': 'triangle'
				}
			},
		],
});

//? image
cy.$('#a').css("background-color", "blue");

cy.viewport({
  zoom: 1,
  pan: { x: 0, y: 0 }
});

//SHOW ADD BUTTON AND ADD NEW NODES
var newNodeIdNum = 1;

//menu
var menu = $('.menu');
//add button
var addButtonStatus = false;
var addButton = $('#addButton');
addButton.css('display','block');
addButton.click( function() {
  addButtonStatus = !addButtonStatus;
  if (addButtonStatus) {
    addButton.css('background','#ce8445');
    cy.on('tap', function() {
        cy.add([
          { group: "nodes", data: {
            id: 'n' + newNodeIdNum,
            name: 'new_decition'+newNodeIdNum,
            text: '',
            url: '',
            root: '' },
            position: { x: 300, y: 200 } }
        ]);
        ++newNodeIdNum;
    })
  } else {
    cy.off('tap');
    addButton.css('background','#ff7f11');
  }
});

//remove button ---------------------------------------------------------
var removeButton = $('#removeButton');
removeButton.css('display','block');
var removeValue = 0;
var removeButtonStatus = false; //status variable
removeButton.click(function() {
  removeButton.css('background','#ce8445');
  removeButtonStatus = !removeButtonStatus;
  if (removeButtonStatus) {
    cy.on('tap', 'node', function(e){
      e.target.remove('node');
      //removeButtonStatus = false;
    });
  } else {
    cy.off('tap');
    removeButton.css('background','#ff7f11');
    //removeButtonStatus = true;
  }

});
//connectEdgeButton -----------------------------------------------------
var connectEdgeButtonStatus = false; //status variable
var connectEdgeButton = $('#connectEdgeButton');
connectEdgeButton.css('display','block');
//CONNECT EDGES
var newEdgeIdNum = 1;
connectEdgeButton.click(function() {
  connectEdgeButtonStatus = !connectEdgeButtonStatus;
  connectEdgeButton.css('background','#ce8445');
  if (connectEdgeButtonStatus) {

    //GET NODE ID-S
    var nodeIndex = 0;
    var nodeArray = [];

    cy.on('tap', 'node', function(e){
      nodeArray[nodeIndex] = e.target.id();
      //click and add border
      var node = e.target;
      node.css('border-style', 'solid');
      node.css('border-width', '2px');
      node.css('border-color', 'purple');

      nodeIndex += 1;
      if(nodeIndex >= 2) {
        nodeIndex = 0;

        if (nodeArray[0] == nodeArray[1]) {
          alert('Same nodes!');
          return false;
        }

        var id = 'e' + newEdgeIdNum;
        ++newEdgeIdNum;
        cy.add([
          { group: "edges", data: { id: 'e' + newEdgeIdNum, source: nodeArray[0], target: nodeArray[1] } }
        ]);

        nodeArray = [];
        node.css('border-width', '0px');
      }
    });
  } else {
    cy.off('tap');
    connectEdgeButton.css('background','#ff7f11');
  }
});

//updateButton --------------------------------------------------------------
var updateButton = $('.updateButton');
updateButton.css('display','block');
var updateButtonStatus = true; //status variable
updateButton.click(function() {
  updateButton.css('background','#ce8445');
  if (updateButtonStatus) {

    //dialog box and update
    cy.on('tap', 'node', function(e){
      var node = e.target;
      $(function() {
        $( "#dialog" ).dialog({
          maxWidth:600,
          maxHeight: 300,
          width: 600,
          height: 300
        });
      });
      document.getElementById('text').value = node.data('text');
      document.getElementById('url').value = node.data('url');

      $('#updateButton').click(function() {
        var newText = document.getElementById('text').value;  node.data('text', newText );
        console.log(node.data('text'));
        var newUrl = document.getElementById('url').value;    node.data('url', newText );
        console.log(node.data('url'));
      });

    });

    document.getElementById('get_file').onclick = function() {
        document.getElementById('my_file').click();
    };

    $('input[type=file]').change(function (e) {
        $('#customfileupload').html($(this).val());
    });

    updateButtonStatus = false;
  } else {
    updateButtonStatus = true;
    cy.off('tap');
    updateButton.css('background','#ff7f11');
  }
});
