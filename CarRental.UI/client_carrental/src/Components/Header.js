import React from 'react';
import History from '../history';
import {Link} from 'react-router-dom';
import {connect} from 'react-redux';
import {logIn} from '../Actions'



const Header=(props)=>{

    return(

        <div>
<Link to='/'>Home</Link>
<Link to='/'>Logout</Link>
<Link to={`/feedback/${1}`}>Fedback</Link>
<Link to={`/userprofile/${props.email}`}>userprofile</Link>    

        </div>
    );
}
const mapStateToProps =(state)=>{
    return{user:state.user,
        email:state.auth.Email
    }
};

export default connect(mapStateToProps,{})(Header);