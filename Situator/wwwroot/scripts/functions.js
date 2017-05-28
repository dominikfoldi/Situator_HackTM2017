var nodes;
var edges;

var cy;

$.ajax({
    url: "http://localhost:58168/api/Courses/1"
}).done(function (data) {
    nodes = data.nodes;

    $.ajax({
        url: "http://localhost:58168/api/NodeRelations/" + data.id,
    }).done(function (data) {
        edges = data;

        cy = cytoscape({
            container: document.getElementById('cy'),
            style: [
                {
                    selector: 'node',
                    css: {
                        'background-color': 'grey',
                        'width': '100px',
                        'height': '100px',
                        label: 'data(title)',
                    }
                },
                {
                    selector: 'edge',
                    css: {
                        'curve-style': 'unbundled-bezier',
                        'target-arrow-shape': 'triangle',
                        'arrow-scale': 3
                    }
                },
            ],
        });

        cy.viewport({
            zoom: 1
       });

        generateNodes(nodes);
        generateEdges(edges);
    })
});


function generateNodes(nodes) {
    for (var nodeId in nodes) {
        var nodeData = nodes[nodeId];
        cy.add([{
            group: "nodes",
            data: {
                id: 'n' + nodeData.id,
                title: nodeData.text,
                nodeData: nodeData
            },
            position: { x: nodeData.positionX, y: nodeData.positionY }
            }
        ]);
        if (nodeData.isRoot) {
            cy.$('#n' +  nodeData.id).css("background-color", "blue");
        }
    }
}

function generateEdges(edges) {
    for (var edgeId in edges) {
        var edgeData = edges[edgeId];
        cy.add([
            {
                group: "edges",
                data: {
                    id: 'e' + edgeData.parentId + '_' + edgeData.childId,
                    source: 'n' + edgeData.parentId,
                    target: 'n' + edgeData.childId
                }
            }
        ]);

    }

}


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
    $("#cy").click(function (e) {
        var nodeData = {
            isRoot: false,
            isLeaf: false,
            positionX: e.offsetX,
            positionY: e.offsetY,
        };

        cy.add([
            {
                group: "nodes",
                data: {
                    id: 'node_temp',
                    nodeData: nodeData,
                },
                position: { x: nodeData.positionX, y: nodeData.positionY }
            }
        ]);
        $.ajax({
            url: "http://localhost:58168/api/Nodes",
            type: "POST",
            data: JSON.stringify(nodeData),
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                var node = $("#node_temp");
                node.data('id', 'n' + result.id);
                node.data('node', result);
            }
        });
         
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
    });
  } else {
    cy.off('tap');
    removeButton.css('background','#ff7f11');
  }

});
//connectEdgeButton -----------------------------------------------------
var connectEdgeButtonStatus = false; //status variable
var connectEdgeButton = $('#connectEdgeButton');
connectEdgeButton.css('display','block');
//CONNECT EDGES
var newEdgeIdNum = 1;
connectEdgeButton.click(function () {
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

        if (nodeArray[0] === nodeArray[1]) {
          alert('Same nodes!');
          return false;
        }

        var id = 'e' + newEdgeIdNum;
        ++newEdgeIdNum;
        cy.add([{
            group: "edges",
            data: {
                id: id,
                source: nodeArray[0],
                target: nodeArray[1]
            }
        }]);

        //console.log(nodeArray[0]);
        //console.log(nodeArray[1]);

        //var parent = $(nodeArray[0]).data().nodeData.id;
        //var child = $(nodeArray[1]).data().nodeData.id
        //var realtion = {
        //    parentId: parent,
        //    childId: child
        //};
        //$.ajax({
        //    url: "http://localhost:58168/api/NodeRelations",
        //    type: "POST",
        //    data: JSON.stringify(realtion),
        //    contentType: 'application/json; charset=utf-8',
        //    success: function (result) {
        //    }
        //});

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

      $('#saveButton').click(function() {
        var newText = document.getElementById('text').value;
        node.data('text', newText);
        console.log(node.data('text'));

        var newUrl = document.getElementById('uploadVideo').value;
        node.data('url', newUrl);
        console.log(node.data('url'));

        //$.ajax({
        //    url: "http://localhost:58168/api/Nodes",
        //    type: "PUT",
        //    data: JSON.stringify(node.data),
        //    contentType: 'application/json; charset=utf-8',
        //    success: function (result) {
        //        //var node = $("#node_temp");
        //        //node.data('id', 'n' + result.id);
        //        //node.data('node', result);
        //    }
        //});

        //var videoData = { 'files': newUrl, 'id' : node.id };

        //$.ajax({
        //    url: "http://localhost:58168/api/Videos",
        //    type: "POST",
        //    data: JSON.stringify(videoData),
        //    success: function (result) {
        //        node.data('url') = result;
        //    }
        //});


      });

    });

    //document.getElementById('get_file').onclick = function() {
    //    document.getElementById('my_file').click();
    //};

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
