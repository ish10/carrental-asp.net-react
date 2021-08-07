import React from 'react';
import History from '../history';
import {Link} from 'react-router-dom'



const Header=()=>{

    return(

        <div>
<Link to='/'>Home</Link>
<Link to='/'>Logout</Link>
<Link to={`/feedback/${1}`}>Fedback</Link>
<Link to='/userprofile'>userprofile</Link>    

        </div>
    );
}

export default Header;