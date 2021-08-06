import React,{useState} from 'react';
import {Link} from 'react-router-dom';
import {connect} from 'react-redux';
import { logIn } from '../Actions';


const LoginForm=(props)=>{
const[LoginDetails,SetLoginDetails]=useState({Email:"",
PasswordHash :"",
})

const onFormSubmit=(event)=>{
    event.preventDefault();
    console.log(LoginDetails);
  props.logIn(LoginDetails);

};
return(<div>
    <h1>Login</h1>

    <form onSubmit={(event)=>onFormSubmit(event)}>
    <label>Email</label>
<input
            type="text"
          value={LoginDetails.Email}
          onChange={(event) => SetLoginDetails((obj)=>({

            ...obj,
            Email :event.target.value

          }))} />
            <br/>
    <label>PasswordHash</label>
    <input
            type="text"
          value={LoginDetails.PasswordHash}
          onChange={(event) => SetLoginDetails((obj)=>({

           ...obj,
           PasswordHash:event.target.value

          }))} />
          <br/>
          <button onClick={(event)=>onFormSubmit(event)}>onclick</button>
          <Link to='/register'>Register</Link>
          </form>
    
    
    
   </div>);

};
export default connect(null,{logIn}) (LoginForm);