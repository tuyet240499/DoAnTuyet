
import React from 'react';
import { 
  NavigationContainer, 
} from '@react-navigation/native';
import { createDrawerNavigator } from '@react-navigation/drawer';
import {createStackNavigator} from '@react-navigation/stack';
import InforCustomScreen from './screens/InforCustomScreen'
import HomeScreen from './screens/HomeScreen';
import CartScreen from './screens/CartScreen';
import SignInScreen from './screens/SignInScreen';
import {DrawerContent} from './screens/DrawerContent';
import FontAwesome from 'react-native-vector-icons/FontAwesome';

const HomeStack = createStackNavigator();
const CartStack = createStackNavigator();
const InforCustomStack = createStackNavigator();

const Drawer = createDrawerNavigator();
const HomeStackScreen =({navigation}) =>(
  <HomeStack.Navigator screenOptions={{
    headerStyle:{
      backgroundColor: "#009387",
    },
    headerTintColor:"#fff",
    headerTitleAlign:'center',
    headerTitleStyle:{
      fontWeight:"bold"
    }
  }}>
    <HomeStack.Screen name="HomeScreen" component={HomeScreen} options={{
      title:"Trang chủ",
      headerLeft:() => (
        <FontAwesome 
        name="bars"
        color='#fff'  
        size={20} onPress={()=>navigation.openDrawer()}
    />
      )
    }}/>
    </HomeStack.Navigator  >
);
const InforCustomStackScreen =({navigation}) =>(
  <InforCustomStack.Navigator screenOptions={{
    headerStyle:{
      backgroundColor: "#009387",
    },
    headerTintColor:"#fff",
    headerTitleAlign:'center',
    headerTitleStyle:{
      fontWeight:"bold"
    }
  }}>
    <InforCustomStack.Screen name="InforCustomScreen" component={InforCustomScreen} options={{
      title:"Thông tin khách hàng",
      headerLeft:() => (
        <FontAwesome 
        name="bars"
        color='#fff'  
        size={20} onPress={()=>navigation.openDrawer()}/>
      )

    }}/>
    </InforCustomStack.Navigator  >
);
const CartStackScreen =({navigation}) =>(
  <CartStack.Navigator screenOptions={{
    headerStyle:{
      backgroundColor: "#009387",
    },
    headerTintColor:"#fff",
    headerTitleAlign:'center',
    headerTitleStyle:{
      fontWeight:"bold", 
    }
  }}>
    <CartStack.Screen name="CartScreen" component={CartScreen} options={{
      title: "Giỏ hàng",
      headerLeft:() => (
        <FontAwesome 
        name="bars"
        color='#fff'  
        size={20} onPress={()=>navigation.openDrawer()}/>
      )
    }}/>
    </CartStack.Navigator >
);
const App = () => {
  return (
    <AuthProvider>
    <NavigationContainer >
       <Drawer.Navigator drawerContent={props => <DrawerContent {...props} />} >
          <Drawer.Screen name="HomeScreen" component={HomeStackScreen} />
          <Drawer.Screen name="CartScreen" component={CartStackScreen} />
          <Drawer.Screen name="InforCustomScreen" component={InforCustomStackScreen} />
        </Drawer.Navigator>
    </NavigationContainer>
    </AuthProvider>
    
  );
}

export default App;
