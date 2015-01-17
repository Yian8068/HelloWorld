$(document).ready(
  $("#div2").click(function(){
    $("#div2").toggle(1000);
  });
  $("#div1").mouseenter(function(){
    $("#div3").fadeto('fast',0.5);
    
  });
  $("#div1").mouseleave(function(){
    $("#div3").fadeto('fast',1);
    
  });
);
