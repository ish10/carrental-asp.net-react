import React from 'react';

import { Router,Route,Switch } from 'react-router-dom';

import Header from './Header';
import history from '../history';
import RegisterForm from './RegisterForm';
import LoginForm from './LoginForm';
import FeedBack from './FeedBack';
import UserProfile from './UserProfile';
import Booking from './Booking'
const App=()=>{

    return(


         
        <Router history={history}>
        <div>
         <Header/>
           <Switch>
           <Route path="/" exact component={LoginForm} />
           <Route path="/register" exact component={RegisterForm} />
           <Route path="/feedback" exact component={FeedBack} />
           <Route path="/userprofile/:id" exact component={UserProfile} />
           <Route path="/booking" exact component={Booking} />

          
           </Switch>
         </div>
         </Router >
      
    );
}
export default App;