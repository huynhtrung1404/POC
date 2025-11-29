import { BrowserRouter, Route, Routes } from "react-router-dom";
import Login from "./features/auths/Login";
import ProtectedRoute from "./routes/ProtectedRoute";
import Home from "./features/admins/home/Home";
import MainLayout from "./components/layout/MainLayout";
import AuthLayout from "./components/layout/AuthLayout";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route
          path="/login"
          element={
            <AuthLayout>
              <Login />
            </AuthLayout>
          }
        />
        <Route element={<ProtectedRoute />}>
          <Route
            path="/"
            element={
              <MainLayout>
                <Home />{" "}
              </MainLayout>
            }
          />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
