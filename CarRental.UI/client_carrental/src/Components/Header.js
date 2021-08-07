import React from 'react';
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
    const onLogout =()=>{
props.signOut();

    };
    return(
        <div className={classes.root}>
        <AppBar position="static">
          <Toolbar>
            
          
            <Typography variant="h6" className={classes.title}>
            <Link to='/'>Home</Link>
            </Typography>
            <Typography variant="h6" className={classes.title}>
            <Link to='/'>Logout</Link>
            </Typography>
            <Typography variant="h6" className={classes.title}>
            <Link to={`/feedback/${1}`}>Fedback</Link>
            </Typography>
            <Typography variant="h6" className={classes.title}>
            <Link to={`/userprofile/${props.email}`}>userprofile</Link>    
            </Typography>
            <Button color="inherit" onClick={(event)=>onLogout(event)}>Logout</Button>
          </Toolbar>
        </AppBar>
      

        
 

        </div>
    );
}
const mapStateToProps =(state)=>{
    return{user:state.user,
        email:state.auth.Email
    }
};

export default connect(mapStateToProps,{signOut})(Header);