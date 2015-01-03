var main=function(){
    
    $('.dropdown-toggle').click(function(){
         $('.dropdown-menu').toggle();
        });
    $('.arrow-next').click(function(){
        var curslide = $('.active-slide');
        var nxtslide = curslide.next();
        var curdot = $('.active-dot');
        var nxtdot = curdot.next();
        if(nxtslide.length==0){
            nxtslide = $('.slide').first();
            nxtdot = $('.dot').first();
            }
        curdot.removeClass('active-dot');
        nxtdot.addClass('active-dot');
        curslide.fadeOut(600).removeClass('active-slide');
        nxtslide.fadeIn(600).addClass('active-slide');        
            
            
        }); 
    $('.arrow-prev').click(function(){
        var curslide = $('.active-slide');
        var nxtslide = curslide.prev();
        var curdot = $('.active-dot');
        var nxtdot = curdot.prev();
        if(nxtslide.length==0){
            nxtslide = $('.slide').last();
            nxtdot = $('.dot').last();
            }
        curdot.removeClass('active-dot');
        nxtdot.addClass('active-dot');
        curslide.fadeOut(600).removeClass('active-slide');
        nxtslide.fadeIn(600).addClass('active-slide');        
            
            
        }); 
}

$(document).ready(main);
