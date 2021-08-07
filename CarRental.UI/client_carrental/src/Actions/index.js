import Streams from "../api/Streams";
import History from '../history'

export const userUpdate = (id, userValues) => {
    return async (dispatch, getState) => {
        const response = await Streams.put(`./api/User/${id}`, { ...userValues });
        dispatch({ type: "UPDATE", payload: response.data });
    };
};

export const userGet =  (id) => {
    return async (dispatch, getState) => {
        const response = await Streams.get(`./api/User/${id}`);
        console.log(response.data);
        dispatch({ type: "USER_GET", payload: response.data });
    };
};

export const logIn = (LoginDetails) => {
    return async (dispatch, getState) => {
        const response = await Streams.post(
            "/api/Account/login",

            { ...LoginDetails }
        );
        console.log(response.data);
        dispatch({ type: "LOG_IN", payload: response.data });
       
            History.push('/booking');
        
    };
};
export const register = (FormValues) => {
    return async (dispatch, getState) => {
        const response = await Streams.post(
            "/api/Account/register",

            { ...FormValues }
        );

        dispatch({ type: "SIGN_IN", payload: response.data });
    };
};
