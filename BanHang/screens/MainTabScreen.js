
import React, { useEffect } from 'react';
import { createDrawerNavigator } from '@react-navigation/drawer';
import {createStackNavigator} from '@react-navigation/stack';
import InforCustomScreen from './InforCustomScreen';
import HomeScreen from './HomeScreen' ;
import CartScreen from './CartScreen';
import FontAwesome from 'react-native-vector-icons/FontAwesome';
import { View, Text, StatusBar } from 'react-native';
import { useTheme } from '@react-navigation/native';

const HomeStack = createStackNavigator();
const CartStack = createStackNavigator();
const InforCustomStack = createStackNavigator();

const MainTabScreen= ({navigation}) => {

  const { colors } = useTheme();

  const theme = useTheme();
  
    return (
      <View >
        <StatusBar barStyle= { theme.dark ? "light-content" : "dark-content" }/>
        <Text >Helo</Text>
      </View>
    );
};

export default MainTabScreen;

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
      <HomeStack.Screen name="Home" component={HomeScreen} options={{
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