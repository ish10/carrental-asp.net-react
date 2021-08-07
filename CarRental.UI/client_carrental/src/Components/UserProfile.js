import React, { useEffect, useState } from "react";
import { connect } from "react-redux";
import { userUpdate, userGet } from "../Actions";
import Streams from "../api/Streams";

const UserDisplay = (props) => {
    const [userValues, setUserValues] = useState({
        UserId: null,
        PasswordHash: "",
        FirstName: "",
        LastName: "",
        Email: "",
        PhoneNumber: "",
    });

    const onFormSubmit = (event) => {
        props.userUpdate(userValues.UserId, userValues);
    };

    const onFormLoad = async (id) => {
        await Streams.get(`/api/User/${id}`).then((response) => {
            console.log(response.data);
            setUserValues({
                ...userValues,
                UserId: response.data.userId,
                PasswordHash: response.data.passwordHash,
                FirstName: response.data.firstName,
                LastName: response.data.lastName,
                PhoneNumber: response.data.phoneNumber,
                Email: response.data.email,
            });
        });
    };

    // const onFormLoad = (id) => {
    //   const response = userGet(id);
    //   debugger;
    // };

    useEffect(() => {
        onFormLoad(1); //Have to replace with id sent from login form
    }, []);

    return (
        <div>
            <h1>User Profile</h1>
            <form onSubmit={(event) => onFormSubmit(event)}>
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
};

export default connect(null, { userUpdate })(UserDisplay);

