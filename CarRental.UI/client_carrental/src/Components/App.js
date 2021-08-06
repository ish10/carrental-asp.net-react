import React from 'react';

import { Router,Route,Switch } from 'react-router-dom';

import Header from './Header';
import history from '../history';
import RegisterForm from './RegisterForm';
import LoginForm from './LoginForm';
const App=()=>{

    return(


         
        <Router history={history}>
        <div>
         <Header/>
           <Switch>
           <Route path="/" exact component={LoginForm} />
           <Route path="/register" exact component={RegisterForm} />
          
           </Switch>
         </div>
         </Router >
      
    );
}
export default App;