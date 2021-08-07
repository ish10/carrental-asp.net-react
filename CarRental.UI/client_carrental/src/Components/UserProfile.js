import React, { useEffect, useState } from "react";
import { connect } from "react-redux";
import { userUpdate, userGet } from "../Actions";
import Streams from "../api/Streams";

const UserDisplay = (props) => {
    const [userValues, setUserValues] = useState({
        UserId: "",
        PasswordHash: "",
        FirstName: "",
        LastName: "",
        Email: "",
        PhoneNumber: "",
    });
    useEffect(()=>{
        props.userGet(1); 
       
        console.log('hi');
        
      
        
        },[])

        useEffect(()=>{
           
            setUserValues({
                ...userValues,
                UserId: props.user.userId,
                PasswordHash: props.user.passwordHash,
                FirstName: props.user.firstName,
                LastName: props.user.lastName,
                PhoneNumber: props.user.phone,
                Email: props.user.email,
            });
          
            
            },[props.user])
     

    const onFormSubmit = (event) => {
        event.preventDefault();
        
      //  props.userUpdate(userValues.UserId, userValues);
    };

   
    //console.log(userValues);
    // const onFormLoad = async (id) => {
    //     await Streams.get(`/api/User/${id}`).then((response) => {
    //         console.log(response.data);


    //     });
    // };

    // const onFormLoad = (id) => {
    //   const response = userGet(id);
    //   debugger;
    // };

    // useEffect(() => {
    //     onFormLoad(1); //Have to replace with id sent from login form
    // }, []);
const Ondisplay=()=>{
 
if (userValues.UserId === "" ){
    
    console.log("me inside renderfunction");
   
    return(<div>loading</div>);

}
else{
    
    console.log(userValues)
    return( <div>
        <h1>User Profile</h1>
        <form>
            <label>First Name</label>
            <input
                type="text"
                value={userValues.FirstName}
                onChange={(event) =>
                    setUserValues((obj) => ({
                        ...obj,
                        FirstName: event.target.value,
                    }))
                }
            />
            <br />
            <label>Last Name</label>
            <input
                type="text"
                value={userValues.LastName}
                onChange={(event) =>
                    setUserValues((obj) => ({
                        ...obj,
                        LastName: event.target.value,
                    }))
                }
            />
            <br />
            <label>Email</label>
            <input
                type="text"
                value={userValues.Email}
                onChange={(event) =>
                    setUserValues((obj) => ({
                        ...obj,
                        Email: event.target.value,
                    }))
                }
            />
            <br />
            <label>Phone Number</label>
            <input
                type="text"
                value={userValues.PhoneNumber}
                onChange={(event) =>
                    setUserValues((obj) => ({
                        ...obj,
                        PhoneNumber: event.target.value,
                    }))
                }
            />
            <br />
            <button onClick={(event) => onFormSubmit(event)}>Submit</button>
        </form>
    </div>);
}


}
    return (
      <div>
       {Ondisplay()}
      </div> 
    );
};
const mapStateToProps =(state)=>{
return{user:state.user}
};
export default connect(mapStateToProps, { userUpdate,userGet  })(UserDisplay);

