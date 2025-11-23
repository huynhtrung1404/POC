import { Button } from "primereact/button";
import { InputText } from "primereact/inputtext";
import { Password } from "primereact/password";
import { useState, type FC } from "react";

const Login: FC = () => {
  const [userName, changedUserName] = useState<string>("");
  const [password, changedPassword] = useState<string>("");

  return (
    <form>
      <div className="field">
        <label htmlFor="userName">User Name</label>
        <InputText
          id="userName"
          type="text"
          value={userName}
          onChange={(e) => changedUserName(e.target.value)}
        />
      </div>
      <div className="field">
        <label htmlFor="password">Password</label>
        <Password
          id="password"
          feedback={false}
          toggleMask
          value={password}
          onChange={(e) => changedPassword(e.target.value)}
        />
      </div>
      <Button type="submit" label="Login" className="mt-2" />
    </form>
  );
};

export default Login;
