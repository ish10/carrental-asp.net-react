const Initial_State = {
    userId: null,
    passwordHash: null,
    firstName: null,
    lastName: null,
    phone: null,
    email: null,
};

export default (state = Initial_State, action) => {
    if (action.type === "UPDATE") {
        return {
            ...state,
            passwordHash: action.payload.passwordHash,
            firstName: action.payload.firstName,
            lastName: action.payload.lastName,
            phone: action.payload.phone,
            email: action.payload.email,
        };
    }
    return state;
};
