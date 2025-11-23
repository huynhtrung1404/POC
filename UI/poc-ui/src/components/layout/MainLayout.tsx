import { Menubar } from "primereact/menubar";
import { Button } from "primereact/button";
import { useNavigate } from "react-router-dom";

interface MainLayoutProps {
  children: React.ReactNode;
}

const MainLayout: React.FC<MainLayoutProps> = ({ children }) => {
  const navigate = useNavigate();

  const items = [
    {
      label: "Dashboard",
      icon: "pi pi-fw pi-home",
      command: () => navigate("/dashboard"),
    },
    {
      label: "Users",
      icon: "pi pi-fw pi-user",
      command: () => alert("Users page coming soon!"),
    },
    {
      label: "Settings",
      icon: "pi pi-fw pi-cog",
      command: () => alert("Settings page coming soon!"),
    },
  ];

  const end = (
    <Button label="Logout" icon="pi pi-sign-out" onClick={() => {}} />
  );

  return (
    <div className="min-h-screen">
      <Menubar model={items} end={end} className="shadow-2" />
      <main className="p-4">{children}</main>
    </div>
  );
};

export default MainLayout;
