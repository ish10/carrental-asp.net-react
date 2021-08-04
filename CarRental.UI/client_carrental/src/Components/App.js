import React from 'react';

import { Router,Route,Switch } from 'react-router-dom';
import FormDisplay from './FormDisplay';
import Header from './Header';
import history from '../history';
const App=()=>{

    return(


         
        <Router history={history}>
        <div>
         <Header/>
           <Switch>
          
           <Route path="/" exact component={FormDisplay} />
          
           </Switch>
         </div>
         </Router >
      
    );
}
export default App;