$(document).ready(
  $("div").click(function(){
    $("div").toggle(1000);
  });
  $("#webtitle").mouseenter(function(){
    $("#div1").fadeto('fast',0.5);
    
  });
  $("#webtitle").mouseleave(function(){
    $("#div1").fadeto('fast',1);
    
  });
);
