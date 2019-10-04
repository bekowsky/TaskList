

function sorting() {
    document.getElementById('search-list').style.display = 'none';  
    document.getElementById('friend-list').style.display = 'block';
    var text = $('#SearchInput').val();
    var parent = $('#friend-list').children();
    var str = '';
    
    for (var i = 0; i < parent.length; i++) {
        if (parent[i].tagName != 'DIV') {
            str = parent[i].getElementsByTagName('a')[0].textContent;

            if (str.indexOf(text) < 0) {
                parent[i].style.display = 'none';
            } else {
                parent[i].style.display = 'block';
            }
        }
    }
      
}