import { BrowserRouter, Route, Routes } from "react-router-dom";
import { AuthLayoutWrapper } from "./utils/WrapperLayout";
import Login from "./features/auths/Login";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route
          path="/login"
          element={
            <AuthLayoutWrapper title="Login">
              <Login />
            </AuthLayoutWrapper>
          }
        />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
