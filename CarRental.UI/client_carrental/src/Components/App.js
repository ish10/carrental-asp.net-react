import React,{useEffect} from 'react';
import { connect } from 'react-redux';

import { Router,Route,Switch } from 'react-router-dom';

import Header from './Header';
import history from '../history';
import RegisterForm from './RegisterForm';
import LoginForm from './LoginForm';
import FeedBack from './FeedBack';
import UserProfile from './UserProfile';
import Booking from './Booking'
import AddCar from './Car/AddCar';
import { deleteCar, getCars, updateCar } from '../Actions';
import CarList from './Car/CarList';

const App=(props)=>{

  useEffect(()=>{
    props.getCars()
   },[])

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
           <Route path="/addCar" exact component={AddCar} />
           <Route path="/carList" exact component={CarList} />
          
           </Switch>
         </div>
         </Router >
      
    );
}
export default connect(null,{getCars})(App);