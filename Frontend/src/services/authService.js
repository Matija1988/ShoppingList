import axios from "axios";
import { httpService, handleSuccess, processError } from "./httpService";

async function logInService(userData) {
  return await axios
    .post("https://localhost:5001/users/login/", userData)
    .then((res) => {
      return handleSuccess(res);
    })
    .catch((e) => {
      return processError(e);
    });
}

export default { logInService };