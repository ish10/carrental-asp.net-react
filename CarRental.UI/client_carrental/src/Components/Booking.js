import React,{useEffect} from 'react';
import {connect} from 'react-redux';
import History  from '../history';
const Booking=(props)=>{
useEffect(()=>{
if(props.auth!==true){
    History.push('/');
}

})

return(<div>carbooking</div>);

}
const mapStateToProps=(state)=>{
   return {auth :state.auth. isSignedIn}
}
export default connect(mapStateToProps,{}) (Booking);