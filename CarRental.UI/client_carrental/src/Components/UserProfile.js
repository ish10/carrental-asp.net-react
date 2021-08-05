import React from "react";
import { useState } from "react";
import { useDispatch } from "react-redux";

const UserProfile = (props) => {
    const [fName, setFName] = useState("");
    const [lName, setLName] = useState("");
    const [phoneNo, setPhone] = useState("");
    const [emailAdd, setEmail] = useState("");

    const dispatch = useDispatch();
    const updateHandler = (event) => {
        event.preventDefault();
        dispatch({
            type: "update",
            firstName: { fName },
            lastName: { lName },
            phone: { phoneNo },
            email: { emailAdd },
        });
    };

    const fNameHandler = (event) => {
        setFName(event.target.value);
    };

    const lNameHandler = (event) => {
        setLName(event.target.value);
    };

    const phoneHandler = (event) => {
        setPhone(event.target.value);
    };

    const emailHandler = (event) => {
        setEmail(event.target.value);
    };

    return (
        <form>
            <div>
                <h2>User Details</h2>
                <div>
                    <label htmlFor="firstName">First Name:</label>
                    <input
                        type="text"
                        name="firstName"
                        id="firstName"
                        defaultValue={props.firstName}
                        onChange={fNameHandler}
                    />
                </div>
                <div>
                    <label htmlFor="lastName">Last Name:</label>
                    <input
                        type="text"
                        name="lastName"
                        id="lastName"
                        defaultValue={props.lastName}
                        onChange={lNameHandler}
                    />
                </div>
                <div>
                    <label htmlFor="phone">Phone Number:</label>
                    <input
                        type="text"
                        name="phone"
                        id="phone"
                        default={props.phone}
                        onChange={phoneHandler}
                    />
                </div>
                <div>
                    <label htmlFor="email">Email:</label>
                    <input
                        type="email"
                        name="email"
                        id="email"
                        default={props.email}
                        onChange={emailHandler}
                    />
                </div>
                <button onClick={updateHandler}>Submit</button>
            </div>
        </form>
    );
};

export default UserProfile;

