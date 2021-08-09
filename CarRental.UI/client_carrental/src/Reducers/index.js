import {combineReducers} from 'redux';
import authReducer from './authReducer';
import userReducer from './userReducer';
import carReducer from './carReducer';

const appReducer= combineReducers({
    auth: authReducer,
    user: userReducer,
    cars: carReducer
    });

    const rootReducer = (state, action) => {
        // when a logout action is dispatched it will reset redux state
        if (action.type === 'SIGN_OUT') {
          state = undefined;
        }
      
        return appReducer(state, action);
      };
      
      export default rootReducer;