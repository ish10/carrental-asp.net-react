import React, { useEffect, useState } from "react";
import { connect } from "react-redux";
import { userUpdate, userGet } from "../Actions";

const UserDisplay = (props) => {
    const [userValues, setUserValues] = useState({
        UserId: "",
        PasswordHash: "",
        FirstName: "",
        LastName: "",
        Email: "",
        PhoneNumber: "",
    });

    useEffect(() => {
        props.userGet(props.email);
        console.log('hi');
    }, [])

    useEffect(() => {

        setUserValues({
            ...userValues,
            UserId: props.user.userId,
            PasswordHash: props.user.passwordHash,
            FirstName: props.user.firstName,
            LastName: props.user.lastName,
            PhoneNumber: props.user.phone,
            Email: props.user.email,
        });


    }, [props.user])


    const onFormSubmit = (event) => {
        event.preventDefault();
        props.userUpdate(userValues.UserId, userValues);
    };

    const Ondisplay = () => {

        if (userValues.UserId === "") {
            return (<div>loading</div>);
        }
        else {
            console.log(userValues)
            return (
                <div>
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
                </div>
            );
        }
    }
    return (
        <div>
            {Ondisplay()}
        </div>
    );
};

const mapStateToProps = (state) => {
    return {
        user: state.user,
        email: state.auth.Email
    }
};

export default connect(mapStateToProps, { userUpdate, userGet })(UserDisplay);
