import Streams from "../api/Streams";
import History from '../history'

export const userUpdate = (id, userValues) => {
    return async (dispatch, getState) => {
        const response = await Streams.put(`./api/User/${id}`, { ...userValues });
        dispatch({ type: "USER_UPDATE", payload: response.data });
    };
};

export const userGet = (email) => {
    return async (dispatch, getState) => {
        const { Token } = getState().auth;
        const response = await Streams.get(`./api/User/${email}`, {
            headers: {
                'Authorization': `Bearer ${Token}`
            }
        });
        console.log(response.data);
        dispatch({ type: "USER_UPDATE", payload: response.data });
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

export const signOut = () => {

    return { type: 'SIGN_OUT' };
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
