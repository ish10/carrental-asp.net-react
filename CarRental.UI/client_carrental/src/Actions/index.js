import Streams from "../api/Streams";
import History from '../history'
import {CREATE_CAR,GET_CARS,UPDATE_CAR,DELETE_CAR,GET_CAR} from './carActionTypes'

export const userUpdate = (id, userValues) => {
    return async (dispatch, getState) => {
        const response = await Streams.put(`./api/User/${id}`, { ...userValues });
        dispatch({ type: "UPDATE", payload: response.data });
    };
};

export const userGet =  (id) => {
    return async (dispatch, getState) => {
        const { Token } = getState().auth;
        const response = await Streams.get(`./api/User/${id}`,{
            headers:{
                'Authorization': `Bearer ${Token}`
            }
        });
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

export const signOut=()=>{

    return{type:'SIGN_OUT'};
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

export const createCar=(FormValues)=>{
    return async(dispatch)=>{
        const response = await Streams.post('/api/Cars',
        {
            carId: 0,
            model: FormValues.model,
            pricePerDay: FormValues.pricePerDay,
            image: FormValues.image,
            numberPlate: FormValues.numberPlate,
            locationId:0,
            location:{
                 locationId:0,
                 city:FormValues.city,
                 province:FormValues.province
            }
        }
        )
        dispatch({type:CREATE_CAR,payload:response.data})
        return response.data
    }
}

export const getCars=()=>{
    return async(dispatch)=>{
        const response = await Streams.get('/api/Cars')
        dispatch({type:GET_CARS,payload:response.data})
    }
}

export const getCar=(id)=>{
    return async(dispatch)=>{
        const response = await Streams.get(`/api/Cars/${id}`)
        dispatch({type:GET_CAR,payload:response.data})
    }
}

export const updateCar=(id,FormValues)=>{
    return async(dispatch)=>{
        const response = await Streams.put(`/api/Cars/${id}`, FormValues);
        dispatch({type:UPDATE_CAR,payload:response.data})
        return response.data
    }
}

export const deleteCar=(id)=>{
    return async(dispatch)=>{
        const response = await Streams.delete(`/api/Cars/${id}`);
        dispatch({type:DELETE_CAR,payload:response.data})
    }
}