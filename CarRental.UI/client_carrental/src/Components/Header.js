import React,{useState,useEffect} from 'react';
import History from '../history';
import {Link} from 'react-router-dom';
import {connect} from 'react-redux';
import {logIn} from '../Actions';
import {signOut}  from '../Actions';
    
import { makeStyles } from '@material-ui/core/styles';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import Button from '@material-ui/core/Button';
import IconButton from '@material-ui/core/IconButton';
import { green, lightGreen, pink, red } from '@material-ui/core/colors';
import '../CSS/menuitem.css'


const useStyles = makeStyles((theme) => ({
  root: {
    flexGrow: 1,
    backgroundColor:pink
  },
  menuButton: {
    marginRight: theme.spacing(2),
  },
  title: {
    flexGrow: 1,
    color: green
  },
  Typography:{
    fontFamily: 'Raleway, Arial',
  }
}));




const Header=(props)=>{
    const classes = useStyles();
    const[menu,setMenu]=useState('false');

    const myFunction=()=>{
setMenu(!menu);

    }
    const username= ()=>{
        if(props.email){
            return props.email
        }
        else return "guest";
    }
const menuitem=()=>{
if(props.signedin){
return(      <div className="dropdown">
<button onClick={()=>myFunction()} className="dropbtn">   {username()}      </button>
<div  className={(menu===true)?"dropdown-content show":"dropdown-content"}>
  <Link to={`/feedback/${1}`}>Feedback</Link>
  <Link to={`/carList`}>Car List</Link>
  <Link to={`/addCar`}>Add Car</Link>
  <Link onClick={()=>myFunction()}to={`/userprofile/${props.email}`}>userprofile</Link> 
  <button onClick={()=>onLogout()}>Logout</button>
  
</div>
</div>);

}
else return(      <div className="dropdown">
<button onClick={()=>myFunction()} className="dropbtn">   {username()}      </button>
<div  className={(menu===true)?"dropdown-content show":"dropdown-content"}>
  
<Link to={`/feedback/${1}`}>Fedback</Link>
  
</div>
</div>);

}
    const onLogout =()=>{
props.signOut();
History.push('/');
    };
    return(
        <div onClick= {()=>myFunction()}className={classes.root}>
        <AppBar position="static">
          <Toolbar>
            
          
            <Typography variant="h6" className={classes.title}>
            <Link to='/'>Home</Link>
            </Typography>
           
            <Typography variant="h6" className={classes.title}>
            <Link to={`/feedback/${1}`}>Feedback</Link>
            </Typography>
            
      {menuitem()}
           
          </Toolbar>
        </AppBar>
      

        
 

        </div>
    );
}
const mapStateToProps =(state)=>{
    return{user:state.user,
        email:state.auth.Email,
        signedin :state.auth.isSignedIn
    }
};

export default connect(mapStateToProps,{signOut})(Header);