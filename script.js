$(document).ready(
  $("#div1").click(function(){
    $("#div1").toggle(1000);
  });
  $("#webtitle").mouseenter(function(){
    $("#div1").fadeto('fast',0.05);
    
  });
  $("#webtitle").mouseleave(function(){
    $("#div1").fadeto('fast',1);
    
  });
);
